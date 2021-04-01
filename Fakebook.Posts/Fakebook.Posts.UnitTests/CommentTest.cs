using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;
using Xunit;
namespace Fakebook.Posts.UnitTests
{

    public class CommentTest
    {
        private const int Id = 1;
        [Fact]
        public void Comment_Constructor_Pass()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Comment newcomment = new Comment(userEmail, Content);

            //assert
            Assert.NotNull(newcomment);
        }


        [Fact]
        public void Comment_EmailIsNotNull()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Comment newcomment = new Comment(userEmail, Content);

            //assert
            Assert.NotNull(newcomment.UserEmail);
        }


        [Fact]
        public void Comment_EmailEquality()
        {

            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Comment newcomment = new Comment(userEmail, Content);

            //assert
            Assert.Equal(userEmail, newcomment.UserEmail);
        }


        [Fact]
        public void Comment_ContentIsNotNull()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Comment newcomment = new Comment(userEmail, Content);

            //assert
            Assert.NotNull(newcomment.Content);
        }

        [Fact]
        public void Comment_EmailisFormattedCorrectly()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Comment newcomment = new Comment(userEmail, Content);

            //assert
            Assert.Contains("@", newcomment.UserEmail);
        }

        public void Comment_EmailFormationThrowsException()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silvertest.com";
            //act



            //assert
            Assert.Throws<ArgumentException>(() => new Comment(userEmail, Content));

        }

    }
}
