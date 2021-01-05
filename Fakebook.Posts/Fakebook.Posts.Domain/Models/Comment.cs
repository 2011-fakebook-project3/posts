using System;

namespace Fakebook.Posts.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public string UserEmail { get; private set; }
        public DateTime CreatedAt { get; set; }

        public Comment(string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                throw new ArgumentException("Email cannot be null or whitespace.", nameof(userEmail));
            }
            UserEmail = userEmail;
        }
    }
}
