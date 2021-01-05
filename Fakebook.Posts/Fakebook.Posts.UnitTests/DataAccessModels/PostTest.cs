using Fakebook.Posts.DataAccess.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Fakebook.Posts.UnitTests.DataAccessModels
{
    public class PostTest
    {
        private readonly Post testPost;

        public PostTest()
        {
            testPost = new Post();
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
        [Fact]
        public void Post_GetUserEmail_EqualsSetValue()
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
        public void Post_GetDate_EqualsSetValue()
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
        public void Post_GetComments_EqualsSetValue()
        {
            //Arrange

            //Act
            var comment = new List<Comment>();
            testPost.Comments = comment;

            //Assert
            Assert.Equal(comment, testPost.Comments);
        }
    }
}