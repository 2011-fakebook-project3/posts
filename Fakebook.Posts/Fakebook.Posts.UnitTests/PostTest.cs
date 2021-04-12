using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;
using Xunit;

namespace Fakebook.Posts.UnitTests
{
    public class PostTests
    {
        private const int Id = 1;

        [Fact]
        public void Post_Constructor_Pass()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Post newpost = new Post(userEmail, Content);
        }

        [Fact]
        public void Post_ContentIsNotNull()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Post newpost = new Post(userEmail, Content);

            //assert
            Assert.NotNull(newpost.Content);
        }

        [Fact]
        public void Post_EmailEquality()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Post newpost = new Post(userEmail, Content);

            //assert
            Assert.Equal(userEmail, newpost.UserEmail);
        }

        /// <summary>
        /// Post_EmailFormationThrowsException_ReturnsArgumentException method checks to ensure that when an email is inputted incorrectly,
        /// an argument exception is thrown.
        /// </summary>
        [Fact]
        public void Post_EmailFormationThrowsException_ReturnsArgumentException()
        {
            const string content = "This is some content";
            const string invalidEmail = "damion.silvertest.com";

            Assert.Throws<ArgumentException>(() => new Post(invalidEmail, content));
        }

        [Fact]
        public void Comment_Constructor_Pass()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            const int PostID = 1;

            //act
            Comment newcomment = new Comment(userEmail, Content, PostID);

            //assert
            Assert.NotNull(newcomment);
        }

        [Fact]
        public void Comment_EmailEquality()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            const int PostID = 1;
            //act
            Comment newcomment = new Comment(userEmail, Content,PostID );

            //assert
            Assert.Equal(userEmail, newcomment.UserEmail);
        }

        [Fact]
        public void Comment_ContentIsNotNull()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            const int PostID = 1;
            //act
            Comment newcomment = new Comment(userEmail, Content,PostID);

            //assert
            Assert.NotNull(newcomment.Content);
        }

        /// <summary>
        /// Comment_EmailFormationThrowsException_ReturnsArgumentException method checks to ensure that when an email is inputted incorrectly,
        /// an argument exception is thrown.
        /// </summary>
        [Fact]
        public void Comment_EmailFormationThrowsException_ReturnsArgumentException()
        {
            const string content = "This is some content";
            const string invalidEmail = "damion.silvertest.com";
            const int PostID = 1;

            Assert.ThrowsAny<ArgumentException>(() => new Comment(invalidEmail, content,PostID));
        }
    }
}
