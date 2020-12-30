using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.Posts.Domain.Models {
    public class Comment {

        public int Id { get; set; }
        public string Content { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        
        public Comment() {

        }
    }
}
