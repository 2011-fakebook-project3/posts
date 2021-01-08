using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain.Interfaces
{

    public interface IPostsRepository : IAsyncEnumerable<Post>, ICollection<Post>
    {
        ValueTask<Post> AddAsync(Post post);

<<<<<<< HEAD
    public interface IPostsRepository {
        ValueTask<Post> GetAsync(int id);
        ValueTask UpdateAsync(Post post);
=======
>>>>>>> b541cf7e9687b37f806a54ec07337faea333667b
    }
}