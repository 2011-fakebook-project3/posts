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
            testComment = new Comment
            {
                Id = 0,
                Content = "Hi",
                UserEmail = "a@b.d",
                CreatedAt = DateTime.Today
            };

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

            testComment.Post = post;

            //Assert
            Assert.Equal(post, testComment.Post);
        }
        
        /// <summary>
        /// Test the property email. Ensures that the model will get and set the email.
        /// </summary>
        [Fact]
        public void Comment_GetEmail_EqualsSetValue()
        {
            //Arrange

            //Act

            testComment.UserEmail = "person@domain.net";

            //Assert
            Assert.Equal("person@domain.net", testComment.UserEmail);
        }
        
        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void Comment_GetCreatedAt_EqualsSetValue()
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