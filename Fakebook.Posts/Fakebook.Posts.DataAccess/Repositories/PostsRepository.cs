using Fakebook.Posts.Domain;
using Fakebook.Posts.Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
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

        public void Add(Post item)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<Domain.Models.Post> AddAsync(Domain.Models.Post post)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(Post item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(Post[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerator<Post> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<Post> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Post item)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}