using System;
using Fakebook.Posts.Domain;
using Fakebook.Posts.Domain.Models;
using Xunit;

namespace Fakebook.Posts.UnitTests.Domain
{
    public class CommentTest : IDisposable
    {
        Comment testComment;
        public CommentTest()
        {
            testComment = new Comment();

        }

        [Fact]
        public void CommentTest1()
        {

            //Act
            testComment.Id = 1;


            //Assert
            Assert.Equal(1, testComment.Id);
        }

        [Fact]
        public void CommentTest2()
        {
            //Arrange

            //Act
            testComment.Content = "Goodman";

            //Assert
            Assert.Equal("Goodman", testComment.Content);
        }
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
        [Fact]
        public void CommentTest4()
        {
            //Arrange

            //Act
            testComment.UserId = 1;

            //Assert
            Assert.Equal(1, testComment.UserId);
        }

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
        public void Dispose()
        {
            testComment = null;
        }
    }
}