using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Posts.Domain;
using Xunit;
using Fakebook.Posts.DataAccess.Mappers;

namespace Fakebook.Posts.UnitTests.DataMapper_Testing
{
    public class DataMapperTest
    {
        [Fact]
        public void DomainPostToDbPost()
        {
            //Arrange
            var domainPost = new Fakebook.Posts.Domain.Models.Post("person1@domain.net", "Content");
            domainPost.CreatedAt = DateTime.Now;

            var domainComment = new Fakebook.Posts.Domain.Models.Comment("person1@domain.net", "Comment Content");
            domainComment.Post = domainPost;
            domainComment.CreatedAt = DateTime.Now;
               
            domainPost.Comments.Add(domainComment);

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
            var dbPost = new Fakebook.Posts.DataAccess.Models.Post
            {

                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now,
                Comments = new HashSet<Fakebook.Posts.DataAccess.Models.Comment>()

            };

            var dbComent = new Fakebook.Posts.DataAccess.Models.Comment
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
            var dbPost = new Fakebook.Posts.DataAccess.Models.Post
            {
                Id = 0,
                UserEmail = "person1@domain.net",
                Content = "Content",
                CreatedAt = DateTime.Now,
                Comments = new HashSet<Fakebook.Posts.DataAccess.Models.Comment>()
            };

            var domainComment = new Fakebook.Posts.Domain.Models.Comment("person1@domain.net", "Comment Content");
            domainComment.CreatedAt = DateTime.Now;

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
            var domainPost = new Fakebook.Posts.Domain.Models.Post("person1@domain.net", "Content");
            domainPost.CreatedAt = DateTime.Now;

            var dbComment = new Fakebook.Posts.DataAccess.Models.Comment
            {
                Content = "Comment Content",
                CreatedAt = DateTime.Now,
                UserEmail = "person2@domain.net",
            };

            //Act
            var domainComment = dbComment.ToDomain(domainPost);

            //Assert
            Assert.True(dbComment.Content == domainComment.Content);
            Assert.Equal(domainPost, domainComment.Post);
            Assert.True(dbComment.CreatedAt == domainComment.CreatedAt);
            Assert.True(dbComment.UserEmail == domainComment.UserEmail);
        }

        [Fact] 
        public void DomainUsertoDbUser()
        {
            //Arrange
            var domainUser = new Fakebook.Posts.Domain.Models.User
            {
                Email = "user@email.net",
                FolloweeEmail = "followee@email.net"
            };

            //Act
            var dbUser = domainUser.ToDataAccess();

            //Assert
            Assert.True(domainUser.Email == dbUser.Email);
            Assert.True(domainUser.FolloweeEmail == domainUser.FolloweeEmail);
        }

        [Fact]
        public void DbUsertoDomainUser()
        {
            //Arrange
            var dbUser = new Fakebook.Posts.DataAccess.Models.User
            {
                Email = "user@email.net",
                FolloweeEmail = "followee@email.net"
            };

            //Act
            var domainUser = dbUser.ToDomain();

            //Assert
            Assert.True(dbUser.Email == domainUser.Email);
            Assert.True(dbUser.FolloweeEmail == domainUser.FolloweeEmail);
        }
    }
}
