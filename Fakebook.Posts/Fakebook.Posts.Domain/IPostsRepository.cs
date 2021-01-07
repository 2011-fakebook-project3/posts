using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain
{
    public interface IPostsRepository : IAsyncEnumerable<Post>, ICollection<Post>
    {
        ValueTask<Post> AddAsync(Post post);

    }
}
