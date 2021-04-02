using System;
using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.DTOs
{
    public class EditCommentDTO
    {
        [Required]
        public int CommentId { get; set; }

        [Required]
        [StringLength(Constants.CommentMaxCommentLength, ErrorMessage = "The edit to this comment is longer than allowed", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}