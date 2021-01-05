using Fakebook.Posts.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fakebook.Posts.UnitTests.Model.Test
{
    public class PostTest
    {
        Post testPost;
        
        public PostTest()
        {
            testPost = new Post("hello@world.edu") {
                Id = 0,
                Content = "Lorem Ipsum",
                Comments = new List<Comment>(),
                Picture = "pic",
                CreatedAt = DateTime.Today
            };

        }
        
        /// <summary>
        /// Test the property Id. Ensures that the model will get and set the id.
        /// </summary>
        [Fact]
        public void Post_GetId_EqualsSetValue()
        {

            //Act
            testPost.Id = 1;

            //Assert
            Assert.Equal(1, testPost.Id);
        }
        
        /// <summary>
        /// Test the property content. Ensures that the model will get and set the content.
        /// </summary>
        [Fact]
        public void Post_GetContent_EqualsSetValue()
        {
            //Arrange

            //Act
            testPost.Content = "Goodman";

            //Assert
            Assert.Equal("Goodman", testPost.Content);
        }
        
        /// <summary>
        /// Test the property picture. Ensures that the model will get and set the picture.
        /// </summary>
        [Fact]
        public void Post_GetPicture_EqualsSetValue()
        {
            //Arrange

            //Act
            testPost.Picture = "picture";


            //Assert
            Assert.Equal("picture", testPost.Picture);
        }
        
        /// <summary>
        /// Test the property email. Ensures that the model will get and set the email.
        /// </summary>
        /*[Fact]
        public void Post_GetUserEmail_EqualsSetValue()
        {
            //Arrange

            //Act
            testPost.UserEmail = "person@domain.net";

            //Assert
            Assert.Equal("person@domain.net", testPost.UserEmail);
        }*/
        
        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void Post_GetCreatedAt_EqualsSetValue()
        {
            //Arrange
            var date = DateTime.Now;

            //Act
            testPost.CreatedAt = date;

            //Assert
            Assert.Equal(date, testPost.CreatedAt);
        }
        
        /// <summary>
        /// Test the property comments. Ensures that the model will get and set the comment and adds it in the list.
        /// </summary>
        [Fact]
        public void Post_GetComments_EqualsSetValue()
        {
            //Arrange
            var comments = new List<Comment>();

            //Act
            testPost.Comments = comments;

            //Assert
            Assert.Equal(comments, testPost.Comments);
        }
    }
}