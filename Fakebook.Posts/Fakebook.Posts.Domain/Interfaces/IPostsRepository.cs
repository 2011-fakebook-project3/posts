using Fakebook.Posts.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fakebook.Posts.Domain.Interfaces
{
    public interface IPostsRepository : IEnumerable<Post>, IAsyncEnumerable<Post>
    {
        ValueTask<Post> AddAsync(Post post);

        ValueTask<Post> GetAsync(int postId);

        ValueTask<Comment> AddCommentAsync(Comment comment);

        ValueTask UpdateAsync(Post post);

        ValueTask DeletePostAsync(int id);

        ValueTask DeleteCommentAsync(int id);

        Task<bool> LikePostAsync(int postId, string userEmail);

        Task<bool> UnlikePostAsync(int postId, string userEmail);

        Task<bool> LikeCommentAsync(int commentId, string userEmail);

        Task<bool> UnlikeCommentAsync(int commentId, string userEmail);
        
        Task<IEnumerable<Post>> NewsfeedAsync(List<string> followingemail, int maxPost=50);

    }
}
