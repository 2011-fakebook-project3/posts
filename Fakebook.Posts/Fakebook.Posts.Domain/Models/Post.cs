﻿using System;
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
        public List<Comment> Comments { get; set; }

        public Post(string userEmail, string content)
        {
            throw new NotImplementedException();
        }
    }
}