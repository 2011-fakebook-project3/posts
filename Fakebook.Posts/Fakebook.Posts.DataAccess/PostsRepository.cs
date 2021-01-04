using System;

namespace Fakebook.Posts.DataModel
{
    public class PostsRepository 
    {
        private readonly FakebookPostsContext _context;
        public PostsRepository(FakebookPostsContext context) {
            _context = context;
        }
    }
}