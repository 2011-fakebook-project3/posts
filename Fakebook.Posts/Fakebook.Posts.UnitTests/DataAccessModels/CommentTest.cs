using Fakebook.Posts.DataAccess.Models;
using System;
using Xunit;

namespace Fakebook.Posts.UnitTests.DataAccessModels
{
    public class CommentTest
    {
        private readonly Comment testComment;

        public CommentTest()
        {
            testComment = new Comment();
        }

        /// <summary>
        /// Test the property Id. Ensures that the model will get and set the id.
        /// </summary>
        [Fact]
        public void Comment_GetId_EqualsSetValue()
        {
            //Act
            testComment.Id = 1;

            //Assert
            Assert.Equal(1, testComment.Id);
        }

        /// <summary>
        /// Test the property content. Ensures that the model will get and set the content.
        /// </summary>
        [Fact]
        public void Comment_GetContent_EqualsSetValue()
        {
            //Arrange

            //Act
            testComment.Content = "Goodman";

            //Assert
            Assert.Equal("Goodman", testComment.Content);
        }

        /// <summary>
        /// Test the property post. Ensures that the model will get and set the post.
        /// </summary>
        [Fact]
        public void Comment_GetPost_EqualsSetValue()
        {
            //Arrange

            //Act
            var post = new Post();
            testComment.Post = post;

            //Assert
            Assert.Equal(post, testComment.Post);
        }

        /// <summary>
        /// Test the property email. Ensures that the model will get and set the email.
        /// </summary>
        [Fact]
        public void Comment_GetUserEmail_EqualsSetValue()
        {
            //Arrange

            //Act
            testComment.UserEmail = "person@domain.net";

            //Assert
            Assert.Equal("person@domain.net", testComment.UserEmail);
        }

        /// <summary>
        /// Test the property post Id. Ensures that the model will get and set the email.
        /// </summary>
        [Fact]
        public void Comment_GetPostId_EqualsSetValue()
        {
            //Arrange

            //Act
            testComment.PostId = 1;

            //Assert
            Assert.Equal(1, testComment.PostId);
        }

        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void Comment_GetDate_EqualsSetValue()
        {
            //Arrange

            //Act
            var date = DateTime.Now;
            testComment.CreatedAt = date;

            //Assert
            Assert.Equal(date, testComment.CreatedAt);
        }
    }
}