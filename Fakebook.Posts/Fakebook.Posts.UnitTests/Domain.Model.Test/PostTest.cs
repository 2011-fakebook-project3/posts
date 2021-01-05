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
            testPost = new Post {
                Id = 0,
                Content = "Lorem Ipsum",
                Comments = new List<Comment>(),
                UserEmail = "hello@world.edu",
                Picture = "pic",
                CreatedAt = DateTime.Today
            };

        }
        
        /// <summary>
        /// Test the property Id. Ensures that the model will get and set the id.
        /// </summary>
        [Fact]
        public void IdSetterTest()
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
        public void ContentSetterTest()
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
        public void PictureSetterTest()
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
        [Fact]
        public void EmailSetterTest()
        {
            //Arrange

            //Act

            testPost.UserEmail = "person@domain.net";
            //Assert
            Assert.Equal("person@domain.net", testPost.UserEmail);
        }
        
        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void CreatedDTSetterTest()
        {
            //Arrange

            //Act
            var date = DateTime.Now;

            testPost.CreatedAt = date;

            //Assert
            Assert.Equal(date, testPost.CreatedAt);
        }
        
        /// <summary>
        /// Test the property comments. Ensures that the model will get and set the comment and adds it in the list.
        /// </summary>
        [Fact]
        public void CommentsSetterTest()
        {
            //Arrange

            //Act
            var comments = new List<Comment>();

            testPost.Comments = comments;

            //Assert
            Assert.Equal(comments, testPost.Comments);
        }
    }
}