// using System;
using System.Collections.Generic;
// using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Fakebook.Posts.Domain.Interfaces;

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
            ) {
            _followsRepository = followsRepository;
            _logger = logger;
        }

        /// <summary>
        /// Follow a user with a given email.
        /// </summary>
        /// <param name="email">
        /// The email of the user to follow.
        /// </param>
        /// <returns></returns>
        [HttpPut("{email}")]
        public async Task<IActionResult> PutTModel(string email)
        {
            // TODO: Your code here
            await Task.Yield();

            return NoContent();
        }

        /// <summary>
        /// Unfollow a user with email "email"
        /// </summary>
        /// <param name="email">
        /// The email of the user to unfollow.
        /// </param>
        /// <returns></returns>
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteTModelById(string email)
        {
            // TODO: Your code here
            await Task.Yield();

            return null;
        }
    }
}