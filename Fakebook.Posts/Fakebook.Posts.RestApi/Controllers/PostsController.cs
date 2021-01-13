using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.RestApi.Controllers {

    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase {

        private readonly IPostsRepository _postsRepository;
        private readonly IFollowsRepository _followsRepository;
        private readonly ILogger<PostsController> _logger;

        public PostsController(
            IPostsRepository postsRepository,
            IFollowsRepository followsRepository, 
            ILogger<PostsController> logger
            ) {
            _postsRepository = postsRepository;
            _followsRepository = followsRepository;
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
        public async Task<IActionResult> PutAsync(int id, Post post) {
            /*try {
                var sessionEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var postEmail = _postsRepository.First(p => p.Id == id).UserEmail;

                if (sessionEmail != postEmail) {
                    return Forbid();
                }
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}");
                return NotFound(e.Message);
            }*/

            try {
                post.Id = id;
                await _postsRepository.UpdateAsync(post);
            } catch (ArgumentException e) {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}");
                return NotFound(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to update a post which resulted in a violation of database constraints.");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Adds a new post to the database.
        /// </summary>
        /// <param name="postModel">The post object to be added.</param>
        /// <returns>
        /// 201Created on successful add, 
        /// 400BadRequest on failure, 
        /// 403Forbidden if post UserEmail does not match the email of the session token.
        /// </returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync(Post postModel) {
            /*var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value; // Get user email from session.

            if (email != postModel.UserEmail) {
                _logger.LogInformation("Authenticated user email did not match user email of the post.");
                return Forbid();
            }*/

            try {
                var created = await _postsRepository.AddAsync(postModel);
                return CreatedAtRoute("Get", new { id = created.Id }, created);
            } catch (ArgumentException e) {
                _logger.LogInformation(e, "Attempted to create a post with invalid arguments.");
                return BadRequest(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to create a post which violated database constraints.");
                return BadRequest(e.Message);
            }
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikePostAsync(int id)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.LikePostAsync(id, email)) return Ok();
            return NotFound();
        }

        [HttpPost("{id}/unlike")]
        public async Task<IActionResult> UnlikePostAsync(int id)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.UnlikePostAsync(id, email)) return Ok();
            return NotFound();
        }

        [HttpPost("{id}/comments/{commentId}/like")]
        public async Task<IActionResult> LikeCommentAsync(int id, int commentId)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            if (await _postsRepository.LikeCommentAsync(commentId, email)) return Ok();
            return NotFound();
        }

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
                .FirstOrDefaultAsync(x => x.Id == id) is Post post)
                return Ok(post);
            return NotFound();
        }
        
        /// <summary>
        /// Get all the posts by the user making the request.
        /// Uses the email in the authorization token.
        /// </summary>
        /// <returns>
        /// The 200 Ok responce with the body of a list of posts
        /// the list can be empty.
        /// </returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value; 
            var userPosts = await _postsRepository
                .Where(x => x.UserEmail == email)
                .AsQueryable().ToListAsync();
            return Ok(userPosts);
        }

        /// <summary>
        /// Get all the posts by a user by their email.
        /// </summary>
        /// <param name="email">The email of the user whos posts are being returned.</param>
        /// <returns>
        /// The 200 Ok responce with the body of a list of posts
        /// the list can be empty.
        /// </returns>
        [HttpGet("{email}")]
        public async Task<IActionResult> GetAsync(string email)
            => Ok( await _postsRepository
                .Where(x => x.UserEmail == email)
                .AsQueryable().ToListAsync() );
        
        /// <summary>
        /// Fetch the posts for a user's newsfeed baised off the session token.
        /// A user's newsfeed contains the three most recent posts 
        /// of the user and the users they follow.
        /// </summary>
        /// <returns>
        /// Ok responce with the top 3 
        /// </returns>
        // Route: api/newsfeed
        [HttpGet("newsfeed")]
        public async Task<IActionResult> GetNewsfeedAsync()
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            var followedUserEmails = _followsRepository.GetFollowedEmails(email);
            followedUserEmails.Add(email);
            // TODO: This query MUST be tested as EF may may not be able to convert it to sql!
            // In case it doesn't work the posts repo will use the sql in NewsfeedAsync.
            var newsfeedPosts = await _postsRepository 
            .Where(p => followedUserEmails.Contains(p.UserEmail) )
            .GroupBy(p => p.UserEmail)
            .SelectMany(g => g.OrderByDescending(p => p.CreatedAt).Take(3) )
            .AsQueryable().ToListAsync();
            return Ok(newsfeedPosts);
        }

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
        public async Task<IActionResult> DeleteAsync(int id) {
            /*try {
                var sessionEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var postEmail = _postsRepository.First(p => p.Id == id).UserEmail;
                if (sessionEmail != postEmail) {
                    return Forbid();
                }
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}.");
                return NotFound(e.Message);
            }*/

            try {
                await _postsRepository.DeletePostAsync(id);
            } catch (ArgumentException e) {
                _logger.LogInformation(e, $"Found no post entry with id: {id}.");
                return NotFound(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Tried to remove post which resulted in a violation of a database constraint");
                return BadRequest(e.Message);
            }

            return NoContent();
        }

    }
}
