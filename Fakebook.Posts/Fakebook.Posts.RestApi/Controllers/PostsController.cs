using Fakebook.Posts.Domain;
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
    public class PostsController : ControllerBase 
    {
        // private readonly IPostsRepository _postsRepository;
        private readonly ILogger<PostsController> _logger;

        public PostsController(
            /*IPostsRepository postsRepository, */
            ILogger<PostsController> logger) 
        {
            // _postsRepository = postsRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) {
            throw new NotImplementedException();
        }

        // [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(Post postModel) {
            throw new NotImplementedException();
        }

        // [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Post postModel) {
            throw new NotImplementedException();
        }

        // [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id) {
            throw new NotImplementedException();
        }

        /*[HttpPost("UploadPicture"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadPicture() {
            throw new NotImplementedException();
        }*/
    }
}
