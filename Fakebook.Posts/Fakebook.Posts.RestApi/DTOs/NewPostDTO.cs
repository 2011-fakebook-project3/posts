using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.Dtos
{
    public class NewPostDto
    {
        [Required]
        [StringLength(Constants.PostMaxLength, ErrorMessage = "New post is too long.", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}
