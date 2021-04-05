using System.ComponentModel.DataAnnotations;

namespace Fakebook.Posts.RestApi.Dtos
{
    public class FollowDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
