using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Dtos;
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
    [Route("api/comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IPostsRepository _postsRepository;
        private readonly ILogger<CommentsController> _logger;
        private readonly ITimeService _timeService;

        public CommentsController(IPostsRepository postsRepository, ILogger<CommentsController> logger, ITimeService timeService)
        {
            _postsRepository = postsRepository;
            _logger = logger;
            _timeService = timeService;
        }

        /// <summary>
        /// Deletes a comment with a given Id.
        /// </summary>
        /// <param name="id">Id of the post to be deleted</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>204 NoContent on success
        /// <br/>400 BadRequest on delete failure
        /// <br/>404 NotFound if the Id did not match an existing post
        /// <br/>403 Forbidden if the UserEmail on the original post does not match the email on the token of the request sender.
        /// </returns>
        [Authorize]
        [HttpDelete("{commentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int commentId)
        {
            try
            {
                var sessionEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
                var post = _postsRepository.AsQueryable().Include(x => x.Comments).First(p => p.Comments.Any(c => c.Id == commentId));
                var comment = post.Comments.First(c => c.Id == commentId);
                if (sessionEmail != post.UserEmail && sessionEmail != comment.UserEmail)
                {
                    return Forbid();
                }
            }
            catch (InvalidOperationException e)
            {
                _logger.LogInformation(e, $"Found no comment entry with Id: {commentId}.");
                return NotFound(e.Message);
            }

            try
            {
                await _postsRepository.DeleteCommentAsync(commentId);
            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e, $"Found no comment entry with id: {commentId}.");
                return NotFound(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Tried to remove comment which resulted in a violation of a database constraint");
                return BadRequest(e.Message);
            }
            return NoContent();
        }

        /// <summary>
        /// Add a new comment to the database.
        /// </summary>
        /// <param name="comment"> NewCommentDto, Properties: Content </param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>201 Created on success
        /// <br/>400 Invalid Argument on comment
        /// <br/>403 Forbidden on user email not matching
        /// </returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(NewCommentDto comment)
        {
            var email = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
    
            Comment created;

            try
            {
                Comment newComment = new Comment(email, comment.Content, comment.PostId);
                newComment.CreatedAt = _timeService.CurrentTime;
                created = await _postsRepository.AddCommentAsync(newComment);
                
            }
            catch (ArgumentException e)
            {
                _logger.LogInformation(e, "Attempted to create a comment with invalid arguments.");
                return BadRequest(e.Message);
            }
            catch (DbUpdateException e)
            {
                _logger.LogInformation(e, "Attempted to create a comment which violated database constraints.");
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(GetAsync), new { id = created.Id }, created);
        }

        /// <summary>
        /// Gets a comment given the comment Id.
        /// </summary>
        /// <param name="id">Comment Id.</param>
        /// <returns>
        /// An IActionResult containing either a:
        /// <br/>200 OK on success.
        /// <br/>404 Not found if comment with id is not found.
        /// </returns>
        [HttpGet("{id}")]
        [ActionName(nameof(GetAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (await _postsRepository.AsQueryable()
                .Include(x => x.Comments).FirstOrDefaultAsync(p =>
                    p.Comments.Any(c => c.Id == id)) is Post post)
            {
                var comment = post.Comments.First(c => c.Id == id);
                return Ok(comment);
            }
            return NotFound();
        }
    }
}
