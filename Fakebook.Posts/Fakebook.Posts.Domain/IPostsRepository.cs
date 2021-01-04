using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.Domain {
    public interface IPostsRepository : ICollection<Post>, IAsyncEnumerable<Post>
    {
        ValueTask<Post> AddAsync(Post post);
    }
}
