using System.Threading.Tasks;
using Fakebook.Posts.Domain.Interfaces;
using System;

namespace Fakebook.Posts.DataAccess.Repositories {
    public class PostsRepository : IPostsRepository {

        private readonly FakebookPostsContext _context;

        public PostsRepository(FakebookPostsContext context) {
            _context = context;
        }

        
    }
}