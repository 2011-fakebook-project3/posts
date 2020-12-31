using Fakebook.Posts.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fakebook.Posts.UnitTests.Model_Test
{
    public class PostTest : IDisposable
    {
        Post testPost;
        public PostTest()
        {
            testPost = new Post();

        }
        /// <summary>
        /// Test the Id. Ensures that the model will get and set the id.
        /// </summary>
        [Fact]
        public void PostTest1()
        {

            //Act
            testPost.Id = 1;


            //Assert
            Assert.Equal(1, testPost.Id);
        }
        /// <summary>
        /// Test the content. Ensures that the model will get and set the content.
        /// </summary>
        [Fact]
        public void PostTest2()
        {
            //Arrange

            //Act
            testPost.Content = "Goodman";

            //Assert
            Assert.Equal("Goodman", testPost.Content);
        }
        /// <summary>
        /// Test the picture. Ensures that the model will get and set the picture.
        /// </summary>
        [Fact]
        public void PostTest3()
        {
            //Arrange

            //Act
            testPost.Picture = "picture";

            //Assert
            Assert.Equal("picture", testPost.Picture);
        }
        /// <summary>
        /// Test the email. Ensures that the model will get and set the email.
        /// </summary>
        [Fact]
        public void PostTest4()
        {
            //Arrange

            //Act
            testPost.UserEmail = "person@domain.net";

            //Assert
            Assert.Equal("person@domain.net", testPost.UserEmail);
        }
        /// <summary>
        /// Test the date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void PostTest5()
        {
            //Arrange

            //Act
            var date = DateTime.Now;
            testPost.CreatedAt = date;

            //Assert
            Assert.Equal(date, testPost.CreatedAt);
        }
        /// <summary>
        /// Test the comments. Ensures that the model will get and set the comment and adds it in the list.
        /// </summary>
        [Fact]
        public void PostTest6()
        {
            //Arrange

            //Act
            var comments = new List<Comment>();
            testPost.Comments = comments;

            //Assert
            Assert.Equal(comments, testPost.Comments);
        }
        /// <summary>
        /// To provide a mechanism to clean up both managed and unmanaged resources.
        /// </summary>  
        public void Dispose()
        {
            testPost = null;
        }
    }
}