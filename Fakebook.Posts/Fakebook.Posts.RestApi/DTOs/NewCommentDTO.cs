using System;
using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.DTOs
{
    public class NewCommentDTO
    {
        [Required]
        [StringLength(Constants.CommentMaxCommentLength, ErrorMessage = "Could not post new comment, too long", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}