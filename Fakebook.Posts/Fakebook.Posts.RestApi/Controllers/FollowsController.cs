using System;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Fakebook.Posts.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly IFollowsRepository _followsRepository;
        private readonly ILogger<FollowsController> _logger;

        public FollowsController(
            IFollowsRepository followsRepository,
            ILogger<FollowsController> logger
            )
        {
            _followsRepository = followsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Fallows a fallower for a folowee given a fallower and fallowee ^^
        /// </summary>
        /// <param name="follow">Follower DTO containing follower and followee</param>
        /// <returns>An IActionResult containing either a:
        /// 204 No Content on success
        /// 400 BadRequest on delete failure</returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Follow follow)
        {
            var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            try
            {
                if (await _followsRepository.AddFollowAsync(new Follow { FollowerEmail = userEmail, FollowedEmail = follow.FollowedEmail }))
                    return NoContent();
                return BadRequest("The user is already being followed.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown while following a user.");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Fallows the given email for the current User.
        /// </summary>
        /// <param name="email">string: Email of person you wish to follow</param>
        /// <returns>An IActionResult containing either a:
        /// 204 No Content on success
        /// 400 BadRequest on delete failure</returns>
        [Authorize]
        [HttpPut("{email}")]
        public async Task<IActionResult> Put(string email)
        {
            var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            try
            {
                if (await _followsRepository.AddFollowAsync(new Follow { FollowerEmail = userEmail, FollowedEmail = email }))
                    return NoContent();
                return BadRequest("The user is already being followed.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown while following a user.");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Unfollows the given email for the current User.
        /// </summary>
        /// <param name="email">string: Email of person you wish to unfollow</param>
        /// <returns>An IActionResult containing either a:
        /// 204 No Content on success
        /// 400 BadRequest on delete failure</returns>
        [Authorize]
        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email)
        {
            var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            try
            {
                if (await _followsRepository.RemoveFollowAsync(new Follow { FollowerEmail = userEmail, FollowedEmail = email }))
                    return NoContent();
                return BadRequest("The user is not being followed.");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown while following a user.");
                return BadRequest(e.Message);
            }
        }
    }
}