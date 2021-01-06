using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Posts.DataModel;
using Fakebook.Posts.Domain;
using Xunit;

namespace Fakebook.Posts.UnitTests.DataMapper_Testing
{
    public class DataMapperTest
    {
        [Fact]
        public void DomainPostToDbPost()
        {
            //Arrange
            var domainPost = new Fakebook.Posts.Domain.Models.Post
            {
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now

            };

            var domainComent = new Fakebook.Posts.Domain.Models.Comment
            {
                Content = "Comment Content",
                Post = domainPost,
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net"
            };

            domainPost.Comments.Add(domainComent);

            //Act
            var dbPost = domainPost.ToDataAccess();

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
            var dbPost = new Fakebook.Posts.DataModel.Post
            {

                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now,
                Comments = new HashSet<Fakebook.Posts.DataModel.Comment>()

            };

            var dbComent = new Fakebook.Posts.DataModel.Comment
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
            var dbPost = new Fakebook.Posts.DataModel.Post
            {
                Id = 0,
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now,
                Comments = new HashSet<Fakebook.Posts.DataModel.Comment>()
            };

            var domainComment = new Fakebook.Posts.Domain.Models.Comment
            {
                Content = "Comment Content",
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net"
            };

            //Act
            var dbComment = domainComment.ToDataAccess(dbPost);

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
            var domainPost = new Fakebook.Posts.Domain.Models.Post
            {
                Id = 0,
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now
            };
            var dbComment = new Fakebook.Posts.DataModel.Comment
            {
                Content = "Comment Content",
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net",
                PostId = 0
            };

            //Act
            var domainComment = dbComment.ToDomain(domainPost);

            //Assert
            Assert.True(dbComment.Content == domainComment.Content);
            Assert.Equal(domainPost, domainComment.Post);
            Assert.True(dbComment.CreatedAt == domainComment.CreatedAt);
            Assert.True(dbComment.UserEmail == domainComment.UserEmail);
        }
    }
}
