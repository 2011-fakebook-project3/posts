using System.Collections.Generic;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.Domain.Interfaces
{
    public interface IFollowsRepository
    {
        ISet<string> GetFollowedEmails(string followerEmail);
        ISet<string> GetFollowerEmails(string followedEmail);
        Task<bool> AddFollowAsync(Follow userFollow);
        Task<bool> RemoveFollowAsync(Follow userFollow);
    }
}