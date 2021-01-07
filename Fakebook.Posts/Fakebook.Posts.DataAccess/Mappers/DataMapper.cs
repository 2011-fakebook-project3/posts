﻿using System;
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
        public static Domain.Models.Post ToDomain(this Post post, bool withComments = true)
        {
            Domain.Models.Post domainPost = new (post.UserEmail, post.Content);
            domainPost.Id = post.Id;
            domainPost.Picture = post.Picture;
            domainPost.CreatedAt = post.CreatedAt.LocalDateTime;
            if (withComments)
                domainPost.Comments = post.Comments
                    .Select(c => c.ToDomain(domainPost)).ToList();

            return domainPost;
        }
        public static Domain.Models.Comment ToDomain(this Comment comment,
            Domain.Models.Post post)
        {
            Domain.Models.Comment domainComment = new (comment.UserEmail, comment.Content);
            domainComment.Id = comment.Id;
            domainComment.Post = post;
            domainComment.CreatedAt = comment.CreatedAt.LocalDateTime;

            return domainComment;
        }

        public static Domain.Models.User ToDomain(this User user)
        {
            return new Domain.Models.User
            {
                Email = user.Email,
                FolloweeEmail = user.FolloweeEmail
            };
        }

        public static Post ToDataAccess(this Domain.Models.Post post)
        {
            Post dbPost = new ();

            dbPost.Id = post.Id;
            dbPost.UserEmail = post.UserEmail;
            dbPost.Content = post.Content;
            dbPost.Picture = post.Picture;
            dbPost.CreatedAt = post.CreatedAt;
            dbPost.Comments = post.Comments.Select(c => c.ToDataAccess(dbPost)).ToList();

            return dbPost;
            
        }
        public static Comment ToDataAccess(this Domain.Models.Comment comment, Post post)
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

        public static User ToDataAccess(this Domain.Models.User user)
        {
            return new User
            {
                Email = user.Email,
                FolloweeEmail = user.FolloweeEmail
            };
        }
    }
}
