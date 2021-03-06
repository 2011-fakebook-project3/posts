﻿using Fakebook.Posts.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain.Interfaces
{
    public interface IPostsRepository : IEnumerable<Post>, IAsyncEnumerable<Post>
    {
        ValueTask<Post> AddAsync(Post post);

        ValueTask<Post> GetAsync(int postId);

        ValueTask<Comment> AddCommentAsync(Comment comment);

        ValueTask UpdateAsync(Post post);

        ValueTask DeletePostAsync(int id);

        Task<Comment> GetCommentAsync(int id);

        ValueTask DeleteCommentAsync(int id);

        Task<List<Post>> GetRecentPostsAsync(string userEmail, int recentInMinutes, DateTime dateNow);

        Task<bool> LikePostAsync(int postId, string userEmail);

        Task<bool> UnlikePostAsync(int postId, string userEmail);

        Task<bool> LikeCommentAsync(int commentId, string userEmail);

        Task<bool> UnlikeCommentAsync(int commentId, string userEmail);

        Task<IEnumerable<Post>> NewsfeedAsync(ICollection<string> followingEmails, int maxPost = 50);
    }
}
