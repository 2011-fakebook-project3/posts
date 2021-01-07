using System;
using System.Collections.Generic;

namespace Fakebook.Posts.DataAccess.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
