using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.DataAccess.Mappers;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> AddFollowAsync(Follow userFollow)
        {
            var dbFollow = userFollow.ToDataAccess();
            if (await _context.Follows.ContainsAsync(dbFollow)) return false;
            await _context.Follows.AddAsync(dbFollow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFollowAsync(Follow userFollow)
        {
            var dbFollow = userFollow.ToDataAccess();
            if (!await _context.Follows.ContainsAsync(dbFollow)) return false;
            _context.Follows.Remove(dbFollow);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
