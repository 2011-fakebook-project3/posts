using System;
using System.Collections.Generic;

namespace Fakebook.Posts.Domain.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }
        public string UserEmail { get; private set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Post(string userEmail, string content)
        {
            if (string.IsNullOrWhiteSpace(userEmail)) throw new ArgumentException("User email is required.", nameof(userEmail));
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Post content is required.", nameof(content));
            UserEmail = userEmail;
            Content = content;
        }
    }
}
