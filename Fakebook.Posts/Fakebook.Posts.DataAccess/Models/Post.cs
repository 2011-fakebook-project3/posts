using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakebookPosts.DataModel
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
        [Required]
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
