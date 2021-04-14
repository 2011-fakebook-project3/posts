﻿using Fakebook.Posts.DataAccess.Mappers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fakebook.Posts.UnitTests.DataMapper_Testing
{
    public class DataMapperTest
    {
        [Fact]
        public void DomainPostToDbPost()
        {
            //Arrange
            Domain.Models.Post domainPost = new("person1@domain.net", "Content")
            {
                CreatedAt = DateTime.Now
            };

            Domain.Models.Comment domainComment = new("person1@domain.net", "Comment Content")
            {
                Post = domainPost,
                CreatedAt = DateTime.Now
            };

            domainPost.Comments.Add(domainComment);
            domainPost.Likes.Add("a@b.d");

            //Act
            var dbPost = domainPost.ToDataAccess();

            //Assert

            Assert.True(dbPost.UserEmail == domainPost.UserEmail);
            Assert.True(dbPost.Content == domainPost.Content);
            Assert.True(dbPost.CreatedAt == domainPost.CreatedAt);
            Assert.True(dbPost.Comments.Count == 1);
            Assert.True(dbPost.PostLikes.Count == 1);
        }

        [Fact]
        public void DbPostToDomainPost()
        {
            //Arrange
            DataAccess.Models.Post dbPost = new()
            {
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now,
                Comments = new HashSet<DataAccess.Models.Comment>(),
                PostLikes = new HashSet<DataAccess.Models.PostLike>()
            };

            DataAccess.Models.Comment dbComent = new()
            {
                Content = "Comment Content",
                Post = dbPost,
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net",
            };

            dbPost.Comments.Add(dbComent);
            dbPost.PostLikes.Add(new DataAccess.Models.PostLike { LikerEmail = "a@b.d", Post = dbPost });

            //Act
            Domain.Models.Post domainPost = dbPost.ToDomain();

            //Assert
            Assert.True(dbPost.UserEmail == domainPost.UserEmail);
            Assert.True(dbPost.Content == domainPost.Content);
            Assert.True(dbPost.CreatedAt == domainPost.CreatedAt);
            Assert.True(dbPost.Comments.Count == 1);
            Assert.True(dbPost.PostLikes.Count == 1);
        }

        [Fact]
        public void DomainCommentToDbComment()
        {
            //Arrange
            DataAccess.Models.Post dbPost = new()
            {
                Id = 0,
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now,
                Comments = new HashSet<DataAccess.Models.Comment>()
            };

            Domain.Models.Comment domainComment = new("person1@domain.net", "Comment Content")
            {
                CreatedAt = DateTime.Now
            };
            domainComment.Likes.Add("a@b.d");

            //Act
            var dbComment = domainComment.ToDataAccess(dbPost);

            //Assert
            Assert.True(dbComment.Content == domainComment.Content);
            Assert.Equal(dbPost, dbComment.Post);
            Assert.True(dbComment.CreatedAt == domainComment.CreatedAt);
            Assert.True(dbComment.UserEmail == domainComment.UserEmail);
            Assert.True(dbComment.CommentLikes.Count == 1);
        }

        [Fact]
        public void DbCommentToDomainComment()
        {
            //Arrange
            Domain.Models.Post domainPost = new("person1@domain.net", "Content")
            {
                CreatedAt = DateTime.Now
            };

            DataAccess.Models.Comment dbComment = new()
            {
                Content = "Comment Content",
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net",
                CommentLikes = new HashSet<DataAccess.Models.CommentLike> { new DataAccess.Models.CommentLike { LikerEmail = "a@b.d" } }
            };

            //Act
            var domainComment = dbComment.ToDomain(domainPost);

            //Assert
            Assert.True(dbComment.Content == domainComment.Content);
            Assert.Equal(domainPost, domainComment.Post);
            Assert.True(dbComment.CreatedAt == domainComment.CreatedAt);
            Assert.True(dbComment.UserEmail == domainComment.UserEmail);
            Assert.True(domainComment.Likes.Count == 1);
        }
    }
}
