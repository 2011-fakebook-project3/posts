﻿using Fakebook.Posts.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase {

        public PostsController(IPostsRepository postsRepository) {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Post postModel) {
            throw new NotImplementedException();
        }

    }
}
