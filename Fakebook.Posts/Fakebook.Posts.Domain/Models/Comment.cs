using System;
using System.Collections.Generic;
using System.Net.Mail;
namespace Fakebook.Posts.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public string UserEmail { get; private set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<string> Likes { get; set; }

        public Comment(string userEmail, string content)
        {
            if (string.IsNullOrWhiteSpace(userEmail)) throw new ArgumentException("User email is required.", nameof(userEmail));
            if (string.IsNullOrWhiteSpace(content)) throw new ArgumentException("Post content is required.", nameof(content));
            try
            {
                userEmail = new MailAddress(userEmail).Address;
            }
            catch (FormatException e)
            {
                throw new ArgumentException("Enter a correct email format.", nameof(userEmail), e);
            }
            UserEmail = userEmail;
            Content = content;
            Likes = new HashSet<string>();
        }
    }
}