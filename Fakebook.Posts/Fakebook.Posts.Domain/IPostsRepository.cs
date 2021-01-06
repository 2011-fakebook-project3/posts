using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain
{
    public interface IPostsRepository : ICollection<Post>, IAsyncEnumerable<Post>
    {
        ValueTask<Post> AddAsync(Post post);
        Task<Post> GetPostByEmail(string email);
        Task<User> GetUserByEmail(string email);

    }
}
