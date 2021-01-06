using System;
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
        public static Fakebook.Posts.Domain.Models.Post ToDomain(this Post post)
        {
            var domainPost = new Fakebook.Posts.Domain.Models.Post(post.UserEmail, post.Content);
            domainPost.Id = post.Id;
            domainPost.Picture = post.Picture;
            domainPost.CreatedAt = post.CreatedAt;
            domainPost.Comments = post.Comments.Select(c => c.ToDomain(domainPost)).ToList();

            return domainPost;
        }
        public static Fakebook.Posts.Domain.Models.Comment ToDomain(this Comment comment,
            Fakebook.Posts.Domain.Models.Post post)
        {
            var domainComment = new Fakebook.Posts.Domain.Models.Comment(comment.UserEmail, comment.Content);
            domainComment.Id = comment.Id;
            domainComment.Post = post;
            domainComment.CreatedAt = comment.CreatedAt;

            return domainComment;
        }
        public static Post ToDataAccess(this Fakebook.Posts.Domain.Models.Post post)
        {
            Post dbPost = new Post();

            dbPost.Id = post.Id;
            dbPost.UserEmail = post.UserEmail;
            dbPost.Content = post.Content;
            dbPost.Picture = post.Picture;
            dbPost.CreatedAt = post.CreatedAt;
            dbPost.Comments = post.Comments.Select(c => c.ToDataAccess(dbPost)).ToList();

            return dbPost;
            
        }
        public static Comment ToDataAccess(this Fakebook.Posts.Domain.Models.Comment comment, Post post)
        {
            var dbComment = new Comment
            {
                Id = comment.Id,
                UserEmail = comment.UserEmail,
                PostId = post.Id,
                Post = post,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };

            return dbComment;
        }
    }
}
