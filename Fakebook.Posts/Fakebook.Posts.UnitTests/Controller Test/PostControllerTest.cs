using Fakebook.Posts.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.UnitTests.Controller_Test
{
    public class PostControllerTest
    {
        [Fact]
        public async Task PostController_PostAsync()
        {
            //Arrange
            var mockRepo = new Mock<IPostsRepository>();
            var postList = new List<Post>();
            var comment = new List<Comment>();
            var date = DateTime.Now;
            var post = new Post()
            {
                Id = 1,
                UserId = 1,
                Comments = comment,
                Content = "Goodman",
                Picture = "picture",
                CreatedAt = date
            };
            postList.Add(post);
            mockRepo.Setup(r => r.AddAsync(It.IsAny<Post>()))
                .Returns(post);
                    
                
        }
       
    }
}
