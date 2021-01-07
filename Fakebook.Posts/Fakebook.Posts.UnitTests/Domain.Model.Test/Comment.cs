using System;
using Xunit;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.UnitTests.Model_Test
{
    public class CommentTest
    {
        /// <summary>
        /// Test the Constructor. 
        /// Ensures that the constructor succeeds with valid arguments
        /// </summary>
        [Fact]
        public void CommentConst_NullEmail()
        {
            //Arrange
            var testContent = "hello world";
            var testEmail = "a@b.c";

            //Assert
            Assert.IsType<Comment>(new Comment(testEmail, testContent));
        }

        /// <summary>
        /// Test the Constructor. 
        /// Ensures that an exception will be raised if the userEmail is null
        /// </summary>
        [Fact]
        public void CommentConst_NullEmail_Exception()
        {
            //Arrange
            var testContent = "hello world";
            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                var myComment = new Comment(null, testContent);
            });
        }

        /// <summary>
        /// Test the Constructor. 
        /// Ensures that an exception will be raised if the content is null
        /// </summary>
        [Fact]
        public void CommentConst_NullContent_Exception()
        {
            //Arrange
            var testEmail = "a@b.c";
            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                var myComment = new Comment(testEmail, null);
            });
        }
    }
}