using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain.Interfaces
{

    public interface IPostsRepository : IAsyncEnumerable<Post>, ICollection<Post>
    {
        ValueTask<Post> AddAsync(Post post);
        ValueTask<Post> GetAsync(int id);
        ValueTask UpdateAsync(Post post);

    }
}