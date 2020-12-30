using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FakebookPosts.DataModel
{
    public class Comment 
    {
        public int Id {get; set;}
        [Required]
        public int UserId {get; set;}
        [Required]
        public int PostId {get; set;}
        [ForeignKey("PostId")]
        public Post Post {get; set;}
        [Required]
        public string Content {get; set;}
        [Required]
        public DateTime CreatedAt {get; set;}  
    }
}