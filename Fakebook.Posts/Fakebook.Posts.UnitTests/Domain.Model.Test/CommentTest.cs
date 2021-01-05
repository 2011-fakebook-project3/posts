using Fakebook.Posts.Domain.Models;
using System;
using Xunit;

namespace Fakebook.Posts.UnitTests.Model.Test
{
    public class CommentTest
    {
        Comment testComment;
        public CommentTest()
        {
            testComment = new Comment("a@b.d")
            {
                Id = 0,
                Content = "Hi",
                CreatedAt = DateTime.Today
            };

        }
        
        /// <summary>
        /// Test the property Id. Ensures that the model will get and set the id.
        /// </summary>
        [Fact]
        public void Comment_GetId_EqualsSetValue()
        {
            // Arrange

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
            var post = new Post("a@b.d");

            //Act
            testComment.Post = post;

            //Assert
            Assert.Equal(post, testComment.Post);
        }
        
        /// <summary>
        /// Test the property email. Ensures that the model will get and set the email.
        /// </summary>
        /*[Fact]
        public void Comment_GetEmail_EqualsSetValue()
        {
            //Arrange

            //Act
            testComment.UserEmail = "person@domain.net";

            //Assert
            Assert.Equal("person@domain.net", testComment.UserEmail);
        }*/
        
        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void Comment_GetCreatedAt_EqualsSetValue()
        {
            //Arrange
            var date = DateTime.Now;

            //Act
            testComment.CreatedAt = date;

            //Assert
            Assert.Equal(date, testComment.CreatedAt);
        }
    }
}