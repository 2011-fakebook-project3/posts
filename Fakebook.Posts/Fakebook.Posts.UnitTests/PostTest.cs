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

            //assert
            Assert.NotNull(newpost);
        }


        [Fact]
        public void Post_EmailIsNotNull()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Post newcomment = new Post(userEmail, Content);

            //assert
            Assert.NotNull(newcomment.UserEmail);
        }


        [Fact]
        public void Post_EmailEquality()
        {

            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Post newcomment = new Post(userEmail, Content);

            //assert
            Assert.Equal(userEmail, newcomment.UserEmail);
        }


        [Fact]
        public void Post_ContentIsNotNull()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Post newcomment = new Post(userEmail, Content);

            //assert
            Assert.NotNull(newcomment.Content);
        }

        [Fact]
        public void Post_EmailisFormattedCorrectly()
        {
            //arrange
            const string Content = "This is some content";
            const string userEmail = "damion.silver@test.com";
            //act
            Post newcomment = new Post(userEmail, Content);

            //assert
            Assert.Contains("@", newcomment.UserEmail);
        }

        [Fact]
        public void Post_EmailFormationThrowsException()
        {

            const string Content = "This is some content";
            const string userEmail = "damion.silvertest.com";

            Assert.Throws<ArgumentException>(() => new Post(userEmail, Content));

        }
    }
}
