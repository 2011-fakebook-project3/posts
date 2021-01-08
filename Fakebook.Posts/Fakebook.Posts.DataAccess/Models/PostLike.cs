namespace Fakebook.Posts.DataAccess.Models
{
    public class PostLike
    {
        public string LikerEmail { get; set; }
        // public int PostId { get; set; }
        public Post Post { get; set; }
    }
}