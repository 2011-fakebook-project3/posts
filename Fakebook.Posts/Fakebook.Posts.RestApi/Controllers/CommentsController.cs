using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.RestApi.Controllers {
    [Route("api/Posts")]
    [ApiController]
    public class CommentsController : ControllerBase {

        // private readonly IPostsRepository _commentsRepository;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(/*ICommentsRepository commentsRepository, */ILogger<CommentsController> logger) {
            // _commentsRepository = commentsRepository;
            _logger = logger;
        }

        // [Authorize]
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> Post(Comment commentModel) {
            throw new NotImplementedException();
        }

        // [Authorize]
        [HttpDelete("{id}/comments")]
        public async Task<IActionResult> Delete(int id) {
            throw new NotImplementedException();
        }
    }
}
