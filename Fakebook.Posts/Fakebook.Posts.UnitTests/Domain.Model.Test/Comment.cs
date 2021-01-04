using System;
using Xunit;

namespace Fakebook.Posts.UnitTests.Model_Test
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
            testComment.Id = 1;


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
            testComment.Content = "Goodman";

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
            testComment.Post = post;

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
            testComment.UserEmail = "person@domain.net";

            //Assert
            Assert.Equal("person@domain.net", testComment.UserEmail);
        }
        /// <summary>
        /// Test the property date. Ensures that the model will get and set the date.
        /// </summary>
        [Fact]
        public void CommentTest5()
        {
            //Arrange

            //Act
            var date = DateTime.Now;
            testComment.CreatedAt = date;

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