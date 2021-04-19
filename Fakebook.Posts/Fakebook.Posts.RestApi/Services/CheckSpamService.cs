using System;
using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;

namespace Fakebook.Posts.RestApi.Services
{
	public class CheckSpamService : ICheckSpamService
	{
		private readonly IPostsRepository _postRepository;
		private readonly ITimeService _timeService;

		public CheckSpamService(IPostsRepository postRepository, ITimeService timeService)
		{
			_postRepository = postRepository;
			_timeService = timeService;
		}

		/// <summary>
		/// Takes in a Post and checks that the post wasn't made too close to another post or the contents are the same as
		/// a recent post.
		/// </summary>
		/// <param name="userPost"> takes in a newly made post to compare for spam</param>
		/// <returns>boolean true for not spam: false for is spam</returns>
		public async Task<bool> IsPostNotSpam(Post userPost)
		{
			if(userPost.Content == null)
			{
				return false;
			}

			// posts can't be the same content within 'recentInMin' minutes
			int recentInMin = 5;
			// can't make new posts within 'secondsTimeout' seconds of another
			int secondsTimeOut = 10;
			var dateNow = _timeService.CurrentTime;
			string userEmail = userPost.UserEmail;
			string userEmailContent = userPost.Content;

			var recentPosts = await _postRepository.GetRecentPostsAsync(userEmail, recentInMin, dateNow);
			if(!CheckTimeSpam(recentPosts, secondsTimeOut, dateNow))
            {
				return false;
            }			
			if(!CheckSamePostSpam(recentPosts, userEmailContent))
            {
				return false;
            }
			return true;
		}

		/// <summary>
		/// checks a Post in comparison to recently made posts of the user to see 
		/// if the post was made too soon to another Post.
		/// </summary>
		/// <param name="posts">A list of recent posts that is retrieved from GetRecentPostsAsync</param>
		/// <param name="secondsTimeout">an amount of seconds until a User can make another Post</param>
		/// <param name="dateNow"> the DateTime 'now' made by TimeService to represent a newly made post</param>
		/// <returns>Boolean: True for no Time Spam: False for post was made too soon to another post</returns>
		public bool CheckTimeSpam(List<Post> posts, int secondsTimeout, DateTime dateNow)
        {
			bool isNotSpam;
			foreach(var post in posts)
            {
				if(dateNow - post.CreatedAt < TimeSpan.FromSeconds(secondsTimeout))
                {
					isNotSpam = false;
					return isNotSpam;
                }
            }
			isNotSpam = true;
			return isNotSpam;
        }

		/// <summary>
		/// checks a Post content in comparison to recently made posts of the user to see
		/// if the post content is the same as another Post.
		/// </summary>
		/// <param name="posts">A list of recently made posts</param>
		/// <param name="userContent">the content of the Users new post to be compared to other posts</param>
		/// <returns>Boolean: True for contents are not the same as others: False for post has same contents
		/// as another post</returns>
		public bool CheckSamePostSpam(List<Post> posts, string userContent)
        {
			bool isNotSpam;
			foreach(var post in posts)
            {
				if(string.Equals(post.Content, userContent))
                {
					isNotSpam = false;
					return isNotSpam;
                }
            }
			isNotSpam = true;
			return isNotSpam;
        }
	}
}