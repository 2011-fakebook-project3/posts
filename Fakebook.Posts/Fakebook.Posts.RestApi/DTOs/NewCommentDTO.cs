using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.Dtos
{
    public class NewCommentDto
    {
        [Required]
        [StringLength(Constants.CommentMaxLength, ErrorMessage = "Could not post new comment, too long", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}
