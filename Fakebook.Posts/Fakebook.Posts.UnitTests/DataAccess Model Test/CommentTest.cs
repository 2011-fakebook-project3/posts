using FakebookPosts.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            public void PostTest5()
            {
                //Arrange

                //Act
                testComment.Id = 1;

                //Assert
                Assert.Equal(1, testComment.Id);
            }
            public void Dispose()
            {
                testComment = null;
            }
        }
}
