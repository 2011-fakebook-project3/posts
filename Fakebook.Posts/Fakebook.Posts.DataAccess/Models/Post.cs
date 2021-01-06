using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fakebook.Posts.DataModel
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
