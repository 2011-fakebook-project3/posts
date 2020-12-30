using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain {
    public interface IPostsRepository : ICollection<Post>, IAsyncEnumerable<Post>
    {
        public ValueTask<bool> AddAsync(Post post);
        // public ValueTask<bool> RemoveAsync(Post post);
    }
}
