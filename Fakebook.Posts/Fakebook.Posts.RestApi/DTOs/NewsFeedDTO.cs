using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Fakebook.Posts.Domain.Constants;

namespace Fakebook.Posts.RestApi.Dtos
{
    public class NewsFeedDTO
    {
        [Required]
        public List<string> Emails { get; set; }
    }
}
