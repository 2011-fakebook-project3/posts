using Fakebook.Posts.DataAccess.Models;
using System;
using Xunit;

namespace Fakebook.Posts.UnitTests.DataAccess_Model_Test
{
    public class CommentTest : IDisposable
    {
        Comment testComment;
        public CommentTest()
        {
            testComment = new Comment();

        }
        /// <summary>
        /// Test the property Id. Ensures that the model will get and set the id.
        /// </summary>
        [Fact]
        public void CommentTest1()
        {

            //Act
            try {
                testComment.Id = 1;
            } catch (NotImplementedException) { }


            //Assert
            Assert.Equal(1, testComment.Id);
        }
        /// <summary>
        /// Test the property content. Ensures that the model will get and set the content.
        /// </summary>
        [Fact]
        public void CommentTest2()
        {
            //Arrange

            //Act
            try {
                testComment.Content = "Goodman";
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal("Goodman", testComment.Content);
        }
        /// <summary>
        /// Test the property post. Ensures that the model will get and set the post.
        /// </summary>
        [Fact]
        public void CommentTest3()
        {
            //Arrange

            //Act
            var post = new Post();
            try {
                testComment.Post = post;
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal(post, testComment.Post);
        }
        /// <summary>
        /// Test the property email. Ensures that the model will get and set the email.
        /// </summary>
        [Fact]
        public void CommentTest4()
        {
            //Arrange

            //Act
            try {
                testComment.UserEmail = "person@domain.net";
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal("person@domain.net", testComment.UserEmail);
        }

        [Fact]
        public void CommentTest5()
        {
            //Arrange

            //Act
            try {
                testComment.PostId = 1;
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal(1, testComment.PostId);
        }
        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void CommentTest6()
        {
            //Arrange

            //Act
            var date = DateTime.Now;
            try {
                testComment.CreatedAt = date;
            } catch (NotImplementedException) { }

            //Assert
            Assert.Equal(date, testComment.CreatedAt);
        }
        /// <summary>
        /// To provide a mechanism to clean up both managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            testComment = null;
        }
    }
}