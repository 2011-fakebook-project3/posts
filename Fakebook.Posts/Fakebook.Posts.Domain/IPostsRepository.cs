using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain {
    public interface IPostsRepository : ICollection<Post>, IAsyncEnumerable<Post>
    {
        ValueTask<bool> AddAsync(Post post);
        // ValueTask<bool> RemoveAsync(Post post);
    }
}
