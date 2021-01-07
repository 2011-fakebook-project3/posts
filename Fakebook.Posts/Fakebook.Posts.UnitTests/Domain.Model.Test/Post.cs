using System;
using System.Collections.Generic;
using Xunit;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.UnitTests.Model_Test
{
    public class PostTest
    {
        /// <summary>
        /// Test the Constructor. 
        /// Ensures that the constructor succeeds with valid arguments
        /// </summary>
        [Fact]
        public void PostConst_NullEmail()
        {
            //Arrange
            var testContent = "hello world";
            var testEmail = "a@b.c";

            //Assert
            Assert.IsType<Post>(new Post(testEmail, testContent));
        }

        /// <summary>
        /// Test the Constructor. 
        /// Ensures that an exception will be raised if the userEmail is null
        /// </summary>
        [Fact]
        public void PostConst_NullEmail_Exception()
        {
            //Arrange
            var testContent = "hello world";
            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                var myPost = new Post(null, testContent);
            });
        }

        /// <summary>
        /// Test the Constructor. 
        /// Ensures that an exception will be raised if the content is null
        /// </summary>
        [Fact]
        public void PostConst_NullContent_Exception()
        {
            //Arrange
            var testEmail = "a@b.c";
            //Assert
            Assert.Throws<ArgumentNullException>(() => {
                var myPost = new Post(testEmail, null);
            });
        }
    }
}