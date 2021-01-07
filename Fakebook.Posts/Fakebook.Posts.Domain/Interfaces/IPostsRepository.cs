using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.Domain.Interfaces
{
    public interface IPostsRepository : IEnumerable<Post>, IAsyncEnumerable<Post>
    {
        ValueTask<Post> AddAsync(Post post);
    }
}
