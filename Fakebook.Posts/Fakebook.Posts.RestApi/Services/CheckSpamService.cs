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

public class CheckSpamService
{
	private readonly IHttpContextAccessor _httpContextAccessor;
	private readonly IPostsRepository _postRepository;

	public CheckSpamService(IHttpContextAccessor httpContextAccessor, IPostsRepository postRepository)
	{
		_httpContextAccessor = httpContextAccessor;
		_postRepository = postRepository;
	}


	// check current post with recent posts where user id = current user
	// will not work until 'Post' has a userId from Auth
	/*
	public async Task<bool> CheckPostSpam(Post userPost)
	{
		int recentInMin = 5;
		var currentUser = _httpContextAccessor.HttpContext.User;
		
		if(currentUser.Identity.IsAuthenticated)
        {
			DateTime dateNow = DateTime.Now;
			var userId = currentUser.Identity;
			int secondsTimeOut = 30;
			// keep user from posting again for certain time
			if (userPost.CreatedAt.AddSeconds(secondsTimeOut) > dateNow)
            {
				return false;
            }
			// needs userId to be inside of 'Post' model
			var recentPosts = await _postRepository.GetRecentPostsAsync(userId, recentInMin);
			foreach(var post in recentPosts)
            {
				if( string.Equals(userPost.Content, post.Content))
                {
					return false;
                }
            }
			return true;
        }
		else
        {
			return false;
        }
	}
	*/
}