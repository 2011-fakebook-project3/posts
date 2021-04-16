using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Fakebook.Posts.RestApi.Services;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Moq;
using Fakebook.Posts.DataAccess.Mappers;

namespace Fakebook.Posts.UnitTests.Services
{
    public class SpamServiceTests
    {

        private readonly IPostsRepository _postRepository;
        private readonly ITimeService _timeService;
        private readonly TimeService timeService = new();

        public string contentIsDuplicate = "this is an old post";
        public string contentIsNotDuplicate = "testing a post";
        public string oldPostContent = "this is an old post";
        public string testUserEmail = "test.user@revature.net";
        public int secondsTimeout = 10;

        public DateTime dateNow;
        public DateTime dateNotRecent = new DateTime(2021, 4, 4);
        
        public SpamServiceTests()
        {
            dateNow = timeService.CurrentTime;
        }

        /// <summary>
        /// Tests CheckTimeSpam method in CheckSpamService comparing a new post with an old post.
        /// asserts true because old post should be older than the secondsTimeout.
        /// </summary>
        [Fact]
        public void CheckSpamService_IsNotSpammingByTime_IsTrue()
        {
       
            Post post = new(testUserEmail, oldPostContent)
            {
                Id = 1,
                Picture = "picture",
                CreatedAt = dateNotRecent
            };

            CheckSpamService checkSpamService = new CheckSpamService(_postRepository, _timeService);
            
            List<Post> oldPosts = new List<Post>();
            oldPosts.Add(post);

            bool isNotSpam = checkSpamService.CheckTimeSpam(oldPosts, secondsTimeout, dateNow);
            Assert.True(isNotSpam);
        }

        /// <summary>
        /// Tests CheckTimeSpam method in CheckSpamService comparing a new post with an old post.
        /// asserts false because the old post is created too soon to the new post.
        /// </summary>
        [Fact]
        public void CheckSpamService_IsNotSpammingByTime_IsFalse()
        {

            Post post = new(testUserEmail, oldPostContent)
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

        /// <summary>
        /// Tests CheckSamePostSpam in CheckSpamService comparing a new posts content with old posts content.
        /// asserts true because the new posts content is not the same as the old posts content.
        /// </summary>
        [Fact]
        public void CheckSpamService_IsNotSpammingByRecentContent_IsTrue()
        {

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

        /// <summary>
        /// Tests CheckSamePostSpam in CheckSpamService comparing a new posts content with an old posts content.
        /// Asserts false because the new posts content is the same as the old posts content.
        /// </summary>
        [Fact]
        public void CheckSpamService_IsNotSpammingByRecentContent_IsFalse()
        {

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

        /// <summary>
        /// Tests CheckSpamService that uses GetRecentPostsAsync to get a list of recent posts
        /// and uses both CheckTimeSpam and CheckSamePostSpam methods together for checking a users post for spam.
        /// Asserts true for GetRecentPostsAsync retrieving lists properly, CheckTimeSpam returning true, and
        /// CheckSamePostSpam returning true.
        /// </summary>
        /// <returns>Task of IsPostNotSpam</returns>
        [Fact]
        public async Task CheckSpamService_IsNotSpammingByTimeAndRecentPosts_IsTrue()
        {

            DateTime now = new DateTime(2020, 2, 2, 2, 2, 2);

            DataAccess.Models.Post dataAccessPost = new()
            {
                Id = 16,
                UserEmail = "testerWoman@yoohoo.net",
                Content = "this is my new post to be tested",
                CreatedAt = now
            };

            DateTime date1 = now - TimeSpan.FromMinutes(3);
            DateTime date2 = now - TimeSpan.FromMinutes(2);
            DateTime date3 = now - TimeSpan.FromMinutes(5);
            DateTime date4 = now - TimeSpan.FromMinutes(40);

            DataAccess.Models.Post addedPost1 = new()
            {
                Id = 10,
                UserEmail = "testerGuy@yoohoo.net",
                Content = "some tasty content",
                CreatedAt = date1
            };

            DataAccess.Models.Post addedPost2 = new()
            {
                Id = 11,
                UserEmail = "testerWoman@yoohoo.net",
                Content = "this content is yummy",
                CreatedAt = date2
            };
            DataAccess.Models.Post addedPost3 = new()
            {
                Id = 15,
                UserEmail = "testerWoman@yoohoo.net",
                Content = "I am hungry for content",
                CreatedAt = date3
            };

            DataAccess.Models.Post addedPost4 = new()
            {
                Id = 13,
                UserEmail = "testerWoman@yoohoo.net",
                Content = "this is old but still tasty content",
                CreatedAt = date4
            };

            List<Post> dbPosts = new List<Post>();
            dbPosts.Add(addedPost1.ToDomain());
            dbPosts.Add(addedPost2.ToDomain());
            dbPosts.Add(addedPost3.ToDomain());
            dbPosts.Add(addedPost4.ToDomain());

            var testedPost = dataAccessPost.ToDomain();
            Mock<IPostsRepository> mockPostRepository = new();
            mockPostRepository.Setup(p => p.GetRecentPostsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(dbPosts);

            Mock<ITimeService> mockTimeService = new();
            mockTimeService.Setup(t => t.CurrentTime).Returns(now);
            CheckSpamService checkSpamService = new CheckSpamService(mockPostRepository.Object, mockTimeService.Object);
            
            bool isPostSpam = await checkSpamService.IsPostNotSpam(testedPost);

            Assert.True(isPostSpam);

        }

    }
}
