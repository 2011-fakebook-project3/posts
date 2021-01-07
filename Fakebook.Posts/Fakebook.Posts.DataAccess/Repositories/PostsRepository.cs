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

        /// <summary>
        /// Updates the content property of the given post in the database. Db column remains unchanged if property value is null.
        /// </summary>
        /// <param name="post">The domain post model containing the updated property values.</param>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        public async ValueTask UpdateAsync(Domain.Models.Post post) {
            if (await _context.Posts.FindAsync(post.Id) is DataAccess.Models.Post current)
            {
                current.Content = post.Content ?? current.Content;
                // Will throw DbUpdateException if a database constraint is violated.
                await _context.SaveChangesAsync(); 
            }
            throw new ArgumentException("Post with given Id not found.", nameof(post.Id));
        }
    }
}
