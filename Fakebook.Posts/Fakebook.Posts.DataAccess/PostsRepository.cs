using Fakebook.Posts.Domain;
using System.Threading.Tasks;

namespace Fakebook.Posts.DataAccess
{
    public class PostsRepository : IPostsRepository
    {
        private readonly FakebookPostsContext _context;
        public PostsRepository(FakebookPostsContext context)
        {
            _context = context;
        }

        public int Count => throw new System.NotImplementedException();

        public bool IsReadOnly => false;
        
        public ValueTask<Domain.Models.Post> AddAsync(Domain.Models.Post post)
        {
            throw new System.NotImplementedException();
        }

    }
}
