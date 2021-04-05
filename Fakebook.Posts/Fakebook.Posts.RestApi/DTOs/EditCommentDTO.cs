using System;
using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.Dtos
{
    public class EditCommentDto
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        [StringLength(Constants.CommentMaxLength, ErrorMessage = "The edit to this comment is longer than allowed", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}