using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Fakebook.Posts.Domain.Interfaces;

namespace Fakebook.Posts.DataAccess.Repositories
{
    public class FollowsRepository : IFollowsRepository
    {
        private readonly FakebookPostsContext _context;
        public FollowsRepository(FakebookPostsContext context)
        {
            _context = context;
        }
        public ISet<string> GetFollowedEmails(string followerEmail) 
            => _context.Follows
                .Where(x => x.FollowerEmail == followerEmail)
                .Select(x => x.FollowedEmail).ToHashSet();

        public ISet<string> GetFollowerEmails(string followedEmail)
            => _context.Follows
                .Where(x => x.FollowedEmail == followedEmail)
                .Select(x => x.FollowerEmail).ToHashSet();
    }
}