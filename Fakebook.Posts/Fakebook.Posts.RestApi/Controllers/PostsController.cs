using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Controllers
{

    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {

        private readonly IPostsRepository _postsRepository;
        private readonly IFollowsRepository _followsRepository;
        private readonly IBlobService _blobService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(
            IPostsRepository postsRepository,
            IFollowsRepository followsRepository,
            IBlobService blobService,
            ILogger<PostsController> logger
            )
        {
            _postsRepository = postsRepository;
            _followsRepository = followsRepository;
            _blobService = blobService;
            _logger = logger;
        }

        /// <summary>
        /// Updates the properties of the resource at the given id to be those contained within the given post object.
        /// </summary>
        /// <param name="id">The Id of the post to be updated.</param>
        /// <param name="post">A post object containing the properties which are to be updated.</param>
        /// <returns>An IActionResult containing either a:
        /// 204NoContent on success,
        /// 400BadRequest on update failure,
        /// 404NotFound if the Id did not match an existing post,
        /// or 403Forbidden if the UserEmail on the original post does not match the email on the token of the request sender.</returns>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, Post post)
        {
            try
            {
                var sessionEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var postEmail = _postsRepository.AsQueryable().First(p => p.Id == id).UserEmail;

                if (sessionEmail != postEmail)
                {
                    return Forbid();
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}");
                return NotFound(e.Message);
            }

            try
            {
                post.Id = id;
                await _postsRepository.UpdateAsync(post);
            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}");
                return NotFound(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Attempted to update a post which resulted in a violation of database constraints.");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a new post to the database.
        /// </summary>
        /// <param name="postModel">The post object to be added.</param>
        /// <returns>201Created on successful add, 400BadRequest on failure, 403Forbidden if post UserEmail does not match the email of the session token.</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PostAsync(Post postModel)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value; // Get user email from session.

            if (email != postModel.UserEmail)
            {
                _logger.LogInformation("Authenticated user email did not match user email of the post.");
                return Forbid();
            }

            Post created;
            try
            {
                created = await _postsRepository.AddAsync(postModel);
            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e, "Attempted to create a post with invalid arguments.");
                return BadRequest(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Attempted to create a post which violated database constraints.");
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(GetAsync), new { id = created.Id }, created);
        }

        [Authorize]
        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikePostAsync(int id)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.LikePostAsync(id, email)) return Ok();
            return NotFound();
        }

        [Authorize]
        [HttpPost("{id}/unlike")]
        public async Task<IActionResult> UnlikePostAsync(int id)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.UnlikePostAsync(id, email)) return Ok();
            return NotFound();
        }

        [Authorize]
        [HttpPost("{id}/comments/{commentId}/like")]
        public async Task<IActionResult> LikeCommentAsync(int id, int commentId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.LikeCommentAsync(commentId, email)) return Ok();
            return NotFound();
        }

        [Authorize]
        [HttpPost("{id}/comments/{commentId}/unlike")]
        public async Task<IActionResult> UnlikeCommentAsync(int id, int commentId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.UnlikeCommentAsync(commentId, email)) return Ok();
            return NotFound();
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (await _postsRepository.AsQueryable()
                .FirstOrDefaultAsync(p => p.Id == id) is Post post) return Ok(post);
            return NotFound();
        }

        [HttpGet("user/{email}")]
        public async Task<IActionResult> GetAsync(string email)
            => Ok(await _postsRepository.AsQueryable()
                .Where(x => x.UserEmail == email).ToListAsync());

        /// <summary>
        /// Deletes the post resource with the given id.
        /// </summary>
        /// <param name="id">Id of the post to be deleted</param>
        /// <returns>An IActionResult containing either a:
        /// 204NoContent on success,
        /// 400BadRequest on delete failure,
        /// 404NotFound if the Id did not match an existing post,
        /// or 403Forbidden if the UserEmail on the original post does not match the email on the token of the request sender.</returns>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var sessionEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var postEmail = _postsRepository.AsQueryable().First(p => p.Id == id).UserEmail;
                if (sessionEmail != postEmail)
                {
                    return Forbid();
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}.");
                return NotFound(e.Message);
            }

            try
            {
                await _postsRepository.DeletePostAsync(id);
            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e, $"Found no post entry with id: {id}.");
                return NotFound(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Tried to remove post which resulted in a violation of a database constraint");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        [HttpPost("UploadPicture"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadPicture(IFormFile file, string userId)
        {
            try
            {
                using (var fileStream = file.OpenReadStream())
                {
                    // generate a random guid from the file name
                    // examplePicture.gif => examplePicture gif
                    var extension = file.FileName.Split('.').Last();
                    var newFileName = $"{userId}-{Guid.NewGuid()}.{extension}";
                    // upload image
                    var result = await _blobService.UploadFileBlobAsync(
                        "fakebook",
                        fileStream,
                        file.ContentType,
                        newFileName);
                    return Ok(new { path = result.AbsoluteUri });
                }
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (Azure.RequestFailedException ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Fetch the posts for a user's newsfeed baised off the session token.
        /// A user's newsfeed contains the three most recent posts 
        /// of the user and the users they follow.
        /// </summary>
        /// <returns>
        /// Ok responce with the top 3 
        /// </returns>
        // Route: api/newsfeed

        [Authorize]
        [HttpGet("newsfeed")]
        public async Task<IActionResult> GetNewsfeedAsync()
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var newsfeedPosts = await _postsRepository.NewsfeedAsync(email, 3);
            // var followedUserEmails = _followsRepository.GetFollowedEmails(email);
            // followedUserEmails.Add(email);
            // // TODO: This query MUST be tested as EF may may not be able to convert it to sql!
            // // In case it doesn't work the posts repo will use the sql in NewsfeedAsync.
            // var newsfeedPosts = await _postsRepository.AsQueryable()
            // .Where(p => followedUserEmails.Contains(p.UserEmail))
            // .GroupBy(p => p.UserEmail)
            // .SelectMany(g => g.OrderByDescending(p => p.CreatedAt).Take(3))
            // .ToListAsync();
            return Ok(newsfeedPosts);
        }
    }
}
