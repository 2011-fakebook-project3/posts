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

		public async Task<bool> CheckPostSpam(Post userPost)
		{
			int recentInMin = 5;
			var dateNow = _timeService.CurrentTime;
			bool isNotSpam = false;
			// very old post default
			DateTime earliestPost = new DateTime(1950, 10, 10, 10, 10, 10);

			string userEmail = userPost.UserEmail;
			// change secondsTimeOut to set how often a user can post
			int secondsTimeOut = 10;



			var recentPosts = await _postRepository.GetRecentPostsAsync(userEmail, recentInMin, dateNow);
			foreach (var post in recentPosts)
			{
				// keep poster from re-posting for secondsTimeOut time
				if(dateNow - post.CreatedAt < TimeSpan.FromSeconds(secondsTimeOut))
                {
					isNotSpam = false;
					return isNotSpam;
                }

				if (string.Equals(userPost.Content, post.Content))
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

