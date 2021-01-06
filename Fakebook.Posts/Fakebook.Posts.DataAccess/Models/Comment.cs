using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fakebook.Posts.DataAccess.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public Post Post { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
