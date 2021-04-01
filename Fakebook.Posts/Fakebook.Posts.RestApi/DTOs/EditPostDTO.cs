using System;
using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.DTOs
{
    public class EditPostDTO
    {
        [Required]
        public int PostId { get; set; }

        [Required]
        [StringLength(Constants.PostMaxLength, ErrorMessage = "The post is longer than allowed", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}