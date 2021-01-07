using Fakebook.Posts.RestApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase {

        private readonly IBlobService _blobService;

        public PostsController(IBlobService blobService) {
            _blobService = blobService;
        }

        [HttpPost("UploadPicture"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadPicture() {
            IFormFile file = Request.Form.Files[0];
            if (file == null) {
                return BadRequest();
            }
            try {
                // generate a random guid from the file name
                string extension = file
                    .FileName
                        .Split('.')
                        .Last();

                string newFileName = $"{Request.Form["userId"]}-{Guid.NewGuid()}.{extension}";

                var result = await _blobService.UploadFileBlobAsync(
                        "fakebook",
                        file.OpenReadStream(),
                        file.ContentType,
                        newFileName);

                var toReturn = result.AbsoluteUri;

                return Ok(new { path = toReturn });
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
