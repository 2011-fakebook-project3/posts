using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Dtos
{
    public class FollowDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}