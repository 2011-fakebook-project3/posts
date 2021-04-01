using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.DTOs
{
    public class CommentDTO
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [StringLength(Constants.MaxCommentLength, ErrorMessage = "The comment is longer than allowed", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}