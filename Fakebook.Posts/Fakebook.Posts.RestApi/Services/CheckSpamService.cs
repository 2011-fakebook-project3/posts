using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Fakebook.Posts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.DataAccess.Repositories;
using Fakebook.Posts.DataAccess;
using Fakebook.Posts.RestApi.Services;

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

		public async Task<bool> IsPostNotSpam(Post userPost)
		{
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

