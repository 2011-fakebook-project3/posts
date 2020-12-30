using System;
using System.Collections.Generic;

namespace Fakebook.Posts.Domain {
    public interface IPostsRepository : ICollection<Post>, IEnumerable<Post>, IAsyncEnumerable<Post>
    {
        public void AddAsync(Post post);
        // public void RemoveAsync(Post post);
    }
}
