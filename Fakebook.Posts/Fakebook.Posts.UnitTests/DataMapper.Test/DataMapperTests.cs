using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakebookPosts.DataModel;
using Fakebook.Posts.Domain;
using Xunit;

namespace Fakebook.Posts.UnitTests.DataMapper_Testing
{
    public class DataMapperTests
    {
        [Fact]
        public void DomainPostToDbPost()
        {
            //Arrange
            Fakebook.Posts.Domain.Models.Post domainPost = new Fakebook.Posts.Domain.Models.Post
            {
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now

            };

            Fakebook.Posts.Domain.Models.Comment domainComent = new Fakebook.Posts.Domain.Models.Comment
            {
                Content = "Comment Content",
                Post = domainPost,
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net"
            };

            domainPost.Comments.Add(domainComent);

            //Act
            FakebookPosts.DataModel.Post dbPost = domainPost.ToDataAccess();

            //Assert

            Assert.True(dbPost.UserEmail == domainPost.UserEmail);
            Assert.True(dbPost.Content == domainPost.Content);
            Assert.True(dbPost.CreatedAt == domainPost.CreatedAt);
            Assert.True(dbPost.Comments.Count == 1);

        }

        [Fact]
        public void DbPostToDomainPost()
        {
            //Arrange
            FakebookPosts.DataModel.Post dbPost = new FakebookPosts.DataModel.Post
            {
                
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now

            };

            FakebookPosts.DataModel.Comment dbComent = new FakebookPosts.DataModel.Comment
            {
                Content = "Comment Content",
                Post = dbPost,
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net"
            };

            dbPost.Comments.Add(dbComent);

            //Act
            Fakebook.Posts.Domain.Models.Post domainPost = dbPost.ToDomain();

            //Assert
            Assert.True(dbPost.UserEmail == domainPost.UserEmail);
            Assert.True(dbPost.Content == domainPost.Content);
            Assert.True(dbPost.CreatedAt == domainPost.CreatedAt);
            Assert.True(dbPost.Comments.Count == 1);
        }


        [Fact]
        public void DomainCommentToDbComment()
        {
            //Arrange
            Fakebook.Posts.Domain.Models.Post domainPost = new Fakebook.Posts.Domain.Models.Post
            {
                Id = 0,
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now

            };

            Fakebook.Posts.Domain.Models.Comment domainComment = new Fakebook.Posts.Domain.Models.Comment
            {
                Content = "Comment Content",
                Post = domainPost,
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net"
            };

            FakebookPosts.DataModel.Post dbPost = domainPost.ToDataAccess();

            //Act
            FakebookPosts.DataModel.Comment dbComment = domainComment.ToDataAccess(dbPost);

            //Assert
            Assert.True(dbComment.Content == domainComment.Content);
            Assert.Equal(dbPost, dbComment.Post);
            Assert.True(dbComment.CreatedAt == domainComment.CreatedAt);
            Assert.True(dbComment.UserEmail == domainComment.UserEmail);
        }

        [Fact]
        public void DbCommentToDomainComment()
        {
            //Arrange
            FakebookPosts.DataModel.Post dbPost = new FakebookPosts.DataModel.Post
            {
                Id = 0,
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now

            };

            FakebookPosts.DataModel.Comment dbComment = new FakebookPosts.DataModel.Comment
            {
                Content = "Comment Content",
                Post = dbPost,
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net",
                PostId = dbPost.Id
                
            };

            Fakebook.Posts.Domain.Models.Post domainPost = dbPost.ToDomain();

            //Act

            Fakebook.Posts.Domain.Models.Comment domainComment = dbComment.ToDomain(domainPost);

            //Assert
            Assert.True(dbComment.Content == domainComment.Content);
            Assert.Equal(domainPost, domainComment.Post);
            Assert.True(dbComment.CreatedAt == domainComment.CreatedAt);
            Assert.True(dbComment.UserEmail == domainComment.UserEmail);

        }

    }
}
