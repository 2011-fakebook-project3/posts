using System.Linq;
using Fakebook.Posts.DataAccess.Models;

namespace Fakebook.Posts.DataAccess.Mappers
{
    /// <summary>
    /// Mapper convert from data access layer models to domain models and back
    /// intended to be called only on posts but can be called on comments
    /// </summary>
    public static class DataMapper
    {
        public static Domain.Models.Post ToDomain(this Post post)
        {
            Domain.Models.Post domainPost = new(post.UserEmail, post.Content);
            domainPost.Id = post.Id;
            domainPost.Picture = post.Picture;
            domainPost.CreatedAt = post.CreatedAt.LocalDateTime;
            if (post.PostLikes is not null)
                domainPost.Likes = post.PostLikes
                .Select(l => l.LikerEmail).ToHashSet();
            if (post.Comments is not null)
                domainPost.Comments = post.Comments
                    .Select(c => c.ToDomain(domainPost)).ToHashSet();

            return domainPost;
        }

        public static Domain.Models.Comment ToDomain(this Comment comment,
            Domain.Models.Post post)
        {
            Domain.Models.Comment domainComment = new(comment.UserEmail, comment.Content);
            domainComment.Id = comment.Id;
            domainComment.Post = post;
            domainComment.CreatedAt = comment.CreatedAt.LocalDateTime;
            if (comment.CommentLikes is not null)
                domainComment.Likes = comment.CommentLikes
                    .Select(l => l.LikerEmail).ToHashSet();
            return domainComment;
        }

        public static Domain.Models.Follow ToDomain(this Follow user)
        {
            return new Domain.Models.Follow
            {
                FollowerEmail = user.FollowerEmail,
                FollowedEmail = user.FollowedEmail
            };
        }

        public static Post ToDataAccess(this Domain.Models.Post post)
        {
            Post dbPost = new();

            dbPost.Id = post.Id;
            dbPost.UserEmail = post.UserEmail;
            dbPost.Content = post.Content;
            dbPost.Picture = post.Picture;
            dbPost.CreatedAt = post.CreatedAt;
            if (post.Comments is not null)
                dbPost.Comments = post.Comments
                    .Select(c => c.ToDataAccess(dbPost.Id)).ToHashSet();
            if (post.Likes is not null)
                dbPost.PostLikes = post.Likes.Select(c =>
                    new PostLike
                    {
                        LikerEmail = c,
                        Post = dbPost,
                        PostId = dbPost.Id
                    }).ToHashSet();
            return dbPost;

        }
        public static Comment ToDataAccess(this Domain.Models.Comment comment, int postId)
        {
            Comment dbComment = new()
            {
                Id = comment.Id,
                UserEmail = comment.UserEmail,
                PostId = postId,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt,
                CommentLikes = comment.Likes.Select(l => new CommentLike { CommentId = comment.Id, LikerEmail = l }).ToHashSet()
            };

            return dbComment;
        }

        public static Follow ToDataAccess(this Domain.Models.Follow user)
        {
            return new Follow
            {
                FollowerEmail = user.FollowerEmail,
                FollowedEmail = user.FollowedEmail
            };
        }
    }
}
