using System;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// Takes in an email of someone to be followed, and follows them for the current user.
        /// </summary>
        /// <param name="follow">Follower DTO containing email of person to be followed</param>
        /// <returns>An IActionResult containing either a:<br></br>
        /// 204 No Content on success<br></br>
        /// 400 BadRequest on delete failure</returns>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync(FollowDto follow)
        {
            var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
            try
            {
                if (await _followsRepository.AddFollowAsync(new Follow { FollowerEmail = userEmail, FollowedEmail = follow.Email }))
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
        /// Follow a user with a given email.
        /// </summary>
        /// <param name="email">string: Email of person you wish to follow</param>
        /// <returns>An IActionResult containing either a:<br></br>
        /// 204 No Content on success<br></br>
        /// 400 BadRequest on delete failure</returns>
        [Authorize]
        [HttpPut("{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutAsync(string email)
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
        /// Unfollow an email for the current user.
        /// </summary>
        /// <param name="email">string: Email of person you wish to unfollow</param>
        /// <returns>An IActionResult containing either a:<br></br>
        /// 204 No Content on success<br></br>
        /// 400 BadRequest on delete failure</returns>
        [Authorize]
        [HttpDelete("{email}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAsync(string email)
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