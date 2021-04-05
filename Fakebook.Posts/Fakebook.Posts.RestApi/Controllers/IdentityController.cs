using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using IdentityModel.Client;
using System.Text.Json;
using System.Net.Http;



namespace Fakebook.Posts.RestApi.Controllers
{
    [Route("identity")]
    [ApiController]
    public class IdentityController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetUserClaim()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

    }
}
