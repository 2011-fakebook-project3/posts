using Fakebook.Posts.DataAccess.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fakebook.Posts.UnitTests.DataAccess_Model_Test
{
    public class PostTest : IDisposable
    {
        Post testPost;
        public PostTest()
        {
            testPost = new Post();

        }
        /// <summary>
        /// Test the property Id. Ensures that the model will get and set the id.
        /// </summary>
        [Fact]
        public void PostTest1()
        {

            //Act
            try {
                testPost.Id = 1;
            } catch (NotImplementedException) { }


            //Assert
            Assert.Equal(1, testPost.Id);
        }
        /// <summary>
        /// Test the property content. Ensures that the model will get and set the content.
        /// </summary>
        [Fact]
        public void PostTest2()
        {
            //Arrange

            //Act
            try {
                testPost.Content = "Goodman";
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal("Goodman", testPost.Content);
        }
        /// <summary>
        /// Test the property picture. Ensures that the model will get and set the picture.
        /// </summary>
        [Fact]
        public void PostTest3()
        {
            //Arrange

            //Act
            try {
                testPost.Picture = "picture";
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal("picture", testPost.Picture);
        }
        /// <summary>
        /// Test the property email. Ensures that the model will get and set the email.
        /// </summary>
        [Fact]
        public void PostTest4()
        {
            //Arrange

            //Act
            try {
                testPost.UserEmail = "person@domain.net";
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal("person@domain.net", testPost.UserEmail);
        }
        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void PostTest5()
        {
            //Arrange

            //Act
            var date = DateTime.Now;
            try {
                testPost.CreatedAt = date;
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal(date, testPost.CreatedAt);
        }
        /// <summary>
        /// Test the property comments. Ensures that the model will get and set the comment and adds it in the list.
        /// </summary>
        [Fact]
        public void PostTest6()
        {
            //Arrange

            //Act
            var comment = new List<Comment>();
            try {
                testPost.Comments = comment;
            } catch (Exception) { }

            //Assert
            Assert.Equal(comment, testPost.Comments);
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