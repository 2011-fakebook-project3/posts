using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase {

        private readonly IPostsRepository _postsRepository;
        private readonly ILogger _logger;

        public PostsController(IPostsRepository postsRepository, ILogger<PostsController> logger) {
            _postsRepository = postsRepository;
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
            try {
                var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var currentPost = await _postsRepository.GetAsync(id);

                if (email != currentPost.UserEmail) {
                    return Forbid();
                }
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}");
                return NotFound(e.Message);
            }

            try {
                post.Id = id;
                await _postsRepository.UpdateAsync(post);
            } catch (InvalidOperationException e) {
                _logger.LogInformation(e, $"Found no post entry with Id: {id}");
                return NotFound(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to update a post which resulted in a violation of database constraints.");
                return BadRequest(e.Message);
            }

            return NoContent();
        }
    }
}
