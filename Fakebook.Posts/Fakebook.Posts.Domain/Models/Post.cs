﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.Posts.Domain.Models {
    public class Post {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserEmail { get; set; }
        
        public ICollection<Comment> Comments { get; set; }

        public Post() {
            Comments = new HashSet<Comment>();
        }
    }
}