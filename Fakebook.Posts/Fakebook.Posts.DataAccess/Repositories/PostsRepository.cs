using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using System;

namespace Fakebook.Posts.DataAccess.Repositories {
    public class PostsRepository : IPostsRepository {

        private readonly FakebookPostsContext _context;

        public PostsRepository(FakebookPostsContext context) {
            _context = context;
        }

        public async ValueTask<Domain.Models.Post> GetAsync(int id) {
            throw new NotImplementedException();
        }

        public async ValueTask UpdateAsync(Domain.Models.Post post) {
            var current = await _context.Posts.FindAsync(post.Id); // Will throw InvalidOperationException if no matching Id is found in the database.

            current.Content = post.Content ?? current.Content;
            current.Picture = post.Picture ?? current.Picture;

            _context.SaveChanges(); // Will throw DbUpdateException if a database constraint is violated.
        }
    }
}