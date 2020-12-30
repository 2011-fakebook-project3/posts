using System;
using System.Collections.Generic;
using System.Text;

namespace Fakebook.Posts.Domain {
    public class Post {

        public int Id { get; set; }
        public string Content { get; set; }
        public string Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        
        public ICollection<Comment> Comments { get; set; }

        public Post() {
            Comments = new HashSet<Comment>();
        }
    }
}
