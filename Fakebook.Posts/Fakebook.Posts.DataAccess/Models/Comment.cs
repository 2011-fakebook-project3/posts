using System;
using System.Collections.Generic;

namespace Fakebook.Posts.DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int PostId { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; }
    }
}