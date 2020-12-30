using System.Linq;

namespace FakebookPosts.DataModel
{
    /// <summary>
    /// Mapper convert from data access layer models to domain models and back
    /// intended to be called only on posts but can be called on comments
    /// </summary>
    public static class DataMapper
    {
        public static Fakebook.Posts.Domain.Models.Post PostDomain2Data(this Post post)
        {
            var postDomain = new Fakebook.Posts.Domain.Models.Post();
            postDomain.Id = post.Id;
            postDomain.UserId = post.UserId;
            postDomain.Picture = post.Picture;
            postDomain.Content = post.Content;
            postDomain.CreatedAt = postDomain.CreatedAt;
            postDomain.Comments = post.Comments.Select(c => c.CommentDomain2Data(postDomain)).ToHashSet();
            return postDomain;
        }
        public static Fakebook.Posts.Domain.Models.Comment CommentDomain2Data(this Comment comment, 
            Fakebook.Posts.Domain.Models.Post post)
        {
            var commentDomain = new Fakebook.Posts.Domain.Models.Comment();
            commentDomain.Id = comment.Id;
            commentDomain.UserId = comment.UserId;
            commentDomain.Post = post;
            commentDomain.CreatedAt = comment.CreatedAt;
            commentDomain.Content = comment.Content;
            return commentDomain;
        }
        public static Post PostData2Domain(Fakebook.Posts.Domain.Models.Post post)
        {
            var postData = new Post();
            postData.Id = post.Id;
            postData.UserId = post.UserId;
            postData.Picture = post.Picture;
            postData.Content = post.Content;
            postData.CreatedAt = post.CreatedAt;
            postData.Comments = post.Comments.Select(c => c.CommentData2Domain(postData)).ToHashSet();
            return postData;
        }
        public static Comment CommentData2Domain(this Fakebook.Posts.Domain.Models.Comment comment, Post post)
        {
            var commentData = new Comment();
            commentData.Id = comment.Id;
            commentData.UserId = comment.UserId;
            commentData.Post = post;
            commentData.CreatedAt = comment.CreatedAt;
            commentData.Content = comment.Content;
            return commentData;
        }
    }
}