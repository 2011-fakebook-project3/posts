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
            if (await _context.Posts.FindAsync(post.Id) is DataAccess.Models.Post current)
            {
                current.Content = post.Content ?? current.Content;
                current.Picture = post.Picture ?? current.Picture;
                // Will throw DbUpdateException if a database constraint is violated.
                await _context.SaveChangesAsync(); 
            }
            throw new ArgumentException("Post with given Id not found", $"{post.Id}");
        }
    }
}
