using System;
using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.DTOs
{
    public class NewPostDTO
    {
        [Required]
        [StringLength(Constants.PostMaxLength, ErrorMessage = "New post is too long.", MinimumLength = Constants.MinimumLength)]
        public string Content { get; set; }
    }
}