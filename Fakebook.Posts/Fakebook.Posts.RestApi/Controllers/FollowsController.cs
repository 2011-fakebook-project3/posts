using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly IFollowsRepository _followsRepository;
        private readonly ILogger<FollowsController> _logger;
        //TODO: Remove Later 
        public string userEmail = "test@email.com";
        public FollowsController(
            IFollowsRepository followsRepository, 
            ILogger<FollowsController> logger
            ) {
            _followsRepository = followsRepository;
            _logger = logger;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostAsync(Follow follow)
        {
            //var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value;
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
        /// Follow a user with a given email.
        /// </summary>
        /// <param name="email">
        /// The email of the user to follow.
        /// </param>
        /// <returns>
        /// NoContent result on success or BadRequest on failure.
        /// </returns>
        [Authorize]
        [HttpPut("{email}")]
        public async Task<IActionResult> PutAsync(string email)
        {
            //var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value; 
            try {
                if (await _followsRepository.AddFollowAsync(new Follow { FollowerEmail = userEmail, FollowedEmail = email }))
                    return NoContent();
                return BadRequest("The user is already being followed."); 
            } catch (Exception e) {
                _logger.LogError(e, "Exception thrown while following a user.");
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Unfollow a user with email "email"
        /// </summary>
        /// <param name="email">
        /// The email of the user to unfollow.
        /// </param>
        /// <returns>
        /// NoContent on success, BadRequest on Failure.
        /// </returns>
        [Authorize]
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteAsync(string email)
        {
            //var userEmail = User.FindFirst(ct => ct.Type.Contains("nameidentifier")).Value; 
            try {
                if (await _followsRepository.RemoveFollowAsync(new Follow { FollowerEmail = userEmail, FollowedEmail = email }))
                    return NoContent();
                return BadRequest("The user is not being followed."); 
            } catch (Exception e) {
                _logger.LogError(e, "Exception thrown while following a user.");
                return BadRequest(e.Message);
            }
        }
    }
}