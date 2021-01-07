using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain
{
    public interface IPostsRepository
    {
        ValueTask<Post> AddAsync(Post post);

    }
}
