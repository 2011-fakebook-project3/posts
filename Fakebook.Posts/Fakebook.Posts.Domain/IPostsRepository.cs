using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.Domain {
    public interface IPostsRepository : ICollection<Post>, IAsyncEnumerable<Post>
    {
        public ValueTask<Post> AddAsync(Post post);
        // public ValueTask<bool> RemoveAsync(Post post);
    }
}
