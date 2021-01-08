using System.Collections.Generic;

namespace Fakebook.Posts.Domain.Interfaces
{
    public interface IFollowsRepository
    {
        ISet<string> GetFollowedEmails(string followerEmail);
        ISet<string> GetFollowerEmails(string followedEmail);
    }
}