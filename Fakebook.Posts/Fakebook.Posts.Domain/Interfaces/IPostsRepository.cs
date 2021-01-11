﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.Domain.Interfaces
{
    public interface IPostsRepository : IEnumerable<Post>, IAsyncEnumerable<Post>
    {
        ValueTask<Post> AddAsync(Post post);
        Task<bool> LikePostAsync(int postId, string userEmail);
        Task<bool> UnlikePostAsync(int postId, string userEmail);
        Task<bool> LikeCommentAsync(int commentId, string userEmail);
        Task<bool> UnlikeCommentAsync(int commentId, string userEmail);

    }
}
