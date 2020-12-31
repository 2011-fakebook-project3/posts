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

        [Fact]
        public void PostTest1()
        {

            //Act
            testPost.Id = 1;


            //Assert
            Assert.Equal(1, testPost.Id);
        }

        [Fact]
        public void PostTest2()
        {
            //Arrange

            //Act
            testPost.Content = "Goodman";

            //Assert
            Assert.Equal("Goodman", testPost.Content);
        }
        [Fact]
        public void PostTest3()
        {
            //Arrange

            //Act
            testPost.Picture = "picture";

            //Assert
            Assert.Equal("picture", testPost.Picture);
        }
        [Fact]
        public void PostTest4()
        {
            //Arrange

            //Act
            testPost.UserEmail = "person@domain.net";

            //Assert
            Assert.Equal("person@domain.net", testPost.UserEmail);
        }
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
        public void Dispose()
        {
            testPost = null;
        }
    }
}