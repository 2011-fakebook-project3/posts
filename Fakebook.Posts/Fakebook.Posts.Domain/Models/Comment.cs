using System;

namespace Fakebook.Posts.Domain.Models
{
    public class Comment
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Content { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Post Post { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserEmail { get => throw new NotImplementedException(); private set => throw new NotImplementedException(); }
        public DateTime CreatedAt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Comment(string userEmail) {
            if (string.IsNullOrWhiteSpace(userEmail)) {
                throw new ArgumentException("Email cannot be null or whitespace.", nameof(userEmail));
            }
            UserEmail = userEmail;
        }
    }
}
