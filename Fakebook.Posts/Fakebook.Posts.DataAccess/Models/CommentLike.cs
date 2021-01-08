namespace Fakebook.Posts.DataAccess.Models
{
    public class CommentLike
    {
        public string LikerEmail { get; set; }
        public int CommentId { get; set; }
        public Comment Comment { get;  set; }
    }
}