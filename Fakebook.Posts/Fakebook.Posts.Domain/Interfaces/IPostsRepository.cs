﻿using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain.Interfaces
{

    public interface IPostsRepository : IAsyncEnumerable<Post>, ICollection<Post>
    {
        ValueTask<Post> AddAsync(Post post);
        Task<bool> LikePost(int postId, string userEmail);
        Task<bool> UnlikePost(int postId, string userEmail);
        Task<bool> LikeComment(int commentId, string userEmail);
        Task<bool> UnlikeComment(int commentId, string userEmail);

    }
}