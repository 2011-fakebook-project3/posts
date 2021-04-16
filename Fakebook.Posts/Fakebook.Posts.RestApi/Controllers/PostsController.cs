using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Dtos;
using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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
        private readonly ITimeService _timeService;
        private readonly ICheckSpamService _checkSpamService;

        public PostsController(
            IPostsRepository postsRepository,
            IFollowsRepository followsRepository,
            IBlobService blobService,
            ILogger<PostsController> logger,
            ITimeService timeService,
            ICheckSpamService checkSpamService
            )
        {
            _postsRepository = postsRepository;
            _followsRepository = followsRepository;
            _blobService = blobService;
            _logger = logger;
            _timeService = timeService;
            _checkSpamService = checkSpamService;
        }

        /// <summary>
        /// Updates the Post's Content given an EditPost DTO
        /// </summary>
        /// <param name="id">The Id of the post to be updated.</param>
        /// <param name="postDTO">EditPostDto containing new content of post.</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>204 No Content on success
        /// <br/>400 Bad Request on update failure
        /// <br/>403 Forbidden if the UserEmail on the original post does not match the email on the token of the request sender.
        /// <br/>404 Not Found if the Id did not match an existing post.
        /// </returns>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(int id, EditPostDto postDTO)
        {
            var sessionEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;

            Post post = new Post(sessionEmail, postDTO.Content);

            post.Id = id;


            try
            { 
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
        /// Adds a new post to the database given a NewPost DTO.
        /// </summary>
        /// <param name="postModel">NewPostDto, Properties: Content </param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>201 Created on success
        /// <br/>400 Bad Request on failure
        /// <br/>403 Forbidden if post UserEmail does not match the email of the session token.
        /// </returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(NewPostDto postModel)
        {

            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value; // Get user email from session.
            Post created;

            try
            {
                Post post = new Post(email, postModel.Content);
                post.CreatedAt = _timeService.CurrentTime;
                bool postNotSpam = await _checkSpamService.IsPostNotSpam(post);
                if (postNotSpam)
                {
                    created = await _postsRepository.AddAsync(post);
                }
                else
                {
                    return BadRequest("Post was created too soon to another, or is the same as previous posts");
                }
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

        /// <summary>
        /// Likes a Post for a User given a post ID.
        /// </summary>
        /// <param name="id">Post ID to be liked</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>200 OK on success
        /// <br/>404 Not Found if Post can't be found
        /// </returns>
        [Authorize]
        [HttpPost("{id}/like")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LikePostAsync(int id)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.LikePostAsync(id, email)) return Ok();
            return NotFound();
        }

        /// <summary>
        /// Unlikes a Post for a User given a post ID.
        /// </summary>
        /// <param name="id">Post ID to be liked</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>200 OK on success
        /// <br/>404 Not Found if Post can't be found
        /// </returns>
        [Authorize]
        [HttpPost("{id}/unlike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnlikePostAsync(int id)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.UnlikePostAsync(id, email)) return Ok();
            return NotFound();
        }

        /// <summary>
        /// Likes a comment for a User given a comment ID.
        /// </summary>
        /// <param name="commentId">Comment ID of comment to be liked</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>200 OK on success
        /// <br/>404 Not Found if Comment can't be found
        /// </returns>
        [Authorize]
        [HttpPost("{id}/comments/{commentId}/like")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LikeCommentAsync(int commentId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.LikeCommentAsync(commentId, email)) return Ok();
            return NotFound();
        }

        /// <summary>
        /// Unlikes a comment for a User given a comment ID.
        /// </summary>
        /// <param name="commentId">Comment ID of comment to be unliked</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>200 OK on success
        /// <br/>404 Not Found if Comment can't be found
        /// </returns>
        [Authorize]
        [HttpPost("{id}/comments/{commentId}/unlike")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UnlikeCommentAsync(int commentId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.UnlikeCommentAsync(commentId, email)) return Ok();
            return NotFound();
        }

        /// <summary>
        /// Gets a post by ID
        /// </summary>
        /// <param name="id">Id of Post</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>200 OK on success
        /// <br/>404 Not Found if Post can't be found
        /// </returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync(int id)
        {
            try
            {
                var post = await _postsRepository.GetAsync(id);
                return Ok(post);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Gets posts for a given user based off email
        /// </summary>
        /// <param name="email">string containing email of person's posts you'd like to view</param>
        /// <returns>
        /// An IActionResult containing a:
        /// <br/>200 OK on success</returns>
        [HttpGet("user/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync(string email)
        {
            return Ok(await _postsRepository.AsQueryable()
                .Where(x => x.UserEmail == email).ToListAsync());
        }

        /// <summary>
        /// Deletes the post resource with the given id.
        /// </summary>
        /// <param name="id">Id of the post to be deleted</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>204 NoContent on success
        /// <br/>400 BadRequest on delete failure
        /// <br/>403 Forbidden if the UserEmail on the original post does not match the email on the token of the request sender.
        /// <br/>404 NotFound if the Id did not match an existing post
        /// </returns>
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UploadPicture(IFormFile file, string userId)
        {
            try
            {
                using var fileStream = file.OpenReadStream();
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
        /// An IActionResult containing a:
        /// <br/>200 OK on success
        /// </returns>
        [Authorize]
        [HttpPost("newsfeed")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetNewsfeedAsync(NewsFeedDto newsfeedemails)
        {
           
                var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                newsfeedemails.Emails.Add(email);


                var newsfeedPosts = await _postsRepository.NewsfeedAsync(newsfeedemails.Emails);
                return Ok(newsfeedPosts);
            
        }
    }
}
