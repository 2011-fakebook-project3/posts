using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Controllers {

    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase {

        private readonly IPostsRepository _postsRepository;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(IPostsRepository postsRepository, ILogger<CommentsController> logger) {
            _postsRepository = postsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Add a new comment to the database. 
        /// </summary>
        /// <param name="comment">
        /// A domain comment. 
        /// </param>
        /// <returns>
        /// The newly created comment
        /// </returns>
        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> PostAsync(Comment comment) {
            /*var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;

            if (email != comment.UserEmail) {
                _logger.LogInformation("Authenticated user email did not match user email of the post.");
                return Forbid();
            }*/

            Comment created;
            try {
                created = await _postsRepository.AddCommentAsync(comment);
            } catch (ArgumentException e) {
                _logger.LogInformation(e, "Attempted to create a comment with invalid arguments.");
                return BadRequest(e.Message);
            } catch (DbUpdateException e) {
                _logger.LogInformation(e, "Attempted to create a comment which violated database constraints.");
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(GetAsync), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetAsync))]
        public async Task<IActionResult> GetAsync(int id) {
            throw new NotImplementedException();
        }
    }
}
