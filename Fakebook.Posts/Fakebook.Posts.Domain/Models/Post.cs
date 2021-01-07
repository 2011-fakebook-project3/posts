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
            if (userEmail == null) throw new ArgumentNullException(nameof(userEmail), "email is required");
            if (content == null) throw new ArgumentNullException(nameof(content), "content is required");
            UserEmail = userEmail;
            Content = content;
        }
    }
}