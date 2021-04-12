using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Fakebook.Posts.RestApi.Services;
using Fakebook.Posts.DataAccess.Repositories;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Fakebook.Posts.RestApi;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;


namespace Fakebook.Posts.UnitTests.Services
{
    public class SpamServiceTests
    {
        

        protected ITimeService _timeService;
        protected IPostsRepository _postRepository;


        TimeService timeService = new TimeService();
        public string contentIsDuplicate = "this is an old post";
        public string contentIsNotDuplicate = "testing a post";
        public string oldPostContent = "this is an old post";

        public string testUserEmail = "test.user@revature.net";

        public int secondsTimeout = 10;
        public DateTime dateNow;
        public DateTime dateNotRecent = new DateTime(2021, 4, 4);


        [Fact]
        public void CheckSpamService_IsNotSpammingByTime_IsTrue()
        {
 
            DateTime dateNow = timeService.CurrentTime;

           
            Post post = new(testUserEmail, oldPostContent)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNotRecent
            };

            Post postPass = new(testUserEmail, contentIsNotDuplicate)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNow
            };

            CheckSpamService checkSpamService = new CheckSpamService(_postRepository, _timeService);
            
            List<Post> oldPosts = new List<Post>();
            oldPosts.Add(post);

            bool isNotSpam = checkSpamService.CheckTimeSpam(oldPosts, secondsTimeout, dateNow);
            Assert.True(isNotSpam);
        }


        [Fact]
        public void CheckSpamService_IsNotSpammingByTime_IsFalse()
        {

            DateTime dateNow = timeService.CurrentTime;

            Post post = new(testUserEmail, oldPostContent)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNow
            };

            Post postPass = new(testUserEmail, contentIsNotDuplicate)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNow
            };

            CheckSpamService checkSpamService = new CheckSpamService(_postRepository, _timeService);

            List<Post> oldPosts = new List<Post>();
            oldPosts.Add(post);

            bool isNotSpam = checkSpamService.CheckTimeSpam(oldPosts, secondsTimeout, dateNow);
            Assert.False(isNotSpam);
        }

        [Fact]
        public void CheckSpamService_IsNotSpammingByRecentContent_IsTrue()
        {

            DateTime dateNow = timeService.CurrentTime;


            Post post = new(testUserEmail, oldPostContent)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNotRecent
            };

            Post postPass = new(testUserEmail, contentIsNotDuplicate)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNow
            };

            CheckSpamService checkSpamService = new CheckSpamService(_postRepository, _timeService);

            List<Post> oldPosts = new List<Post>();
            oldPosts.Add(post);

            bool isNotSpam = checkSpamService.CheckSamePostSpam(oldPosts, postPass.Content);
            Assert.True(isNotSpam);
        }

        [Fact]
        public void CheckSpamService_IsNotSpammingByRecentContent_IsFalse()
        {

            DateTime dateNow = timeService.CurrentTime;

            Post post = new(testUserEmail, oldPostContent)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNotRecent
            };

            Post postPass = new(testUserEmail, contentIsDuplicate)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNow
            };

            CheckSpamService checkSpamService = new CheckSpamService(_postRepository, _timeService);

            List<Post> oldPosts = new List<Post>();
            oldPosts.Add(post);

            bool isNotSpam = checkSpamService.CheckSamePostSpam(oldPosts, postPass.Content);
            Assert.False(isNotSpam);
        }








    }
}
