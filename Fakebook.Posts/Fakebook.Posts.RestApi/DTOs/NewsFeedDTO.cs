using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Fakebook.Posts.RestApi.Dtos
{
    public class NewsFeedDto
    {
        [Required]
        public ICollection<string> Emails { get; set; }
    }
}
