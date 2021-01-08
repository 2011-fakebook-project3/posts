using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain.Interfaces
{

    public interface IPostsRepository : IAsyncEnumerable<Post>, ICollection<Post>
    {
        ValueTask<Post> AddAsync(Post post);
        Task<bool> LikePostAsync(int postId, string userEmail);
        Task<bool> UnlikePostAsync(int postId, string userEmail);
        Task<bool> LikeCommentAsync(int commentId, string userEmail);
        Task<bool> UnlikeCommentAsync(int commentId, string userEmail);

    }
}