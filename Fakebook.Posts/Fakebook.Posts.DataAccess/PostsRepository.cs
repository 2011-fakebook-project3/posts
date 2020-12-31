using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Fakebook.Posts.Domain;
using Fakebook.Posts.Domain.Models;

namespace FakebookPosts.DataModel
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

        public void Add(Fakebook.Posts.Domain.Models.Post item)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<Fakebook.Posts.Domain.Models.Post> AddAsync(Fakebook.Posts.Domain.Models.Post post)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(Fakebook.Posts.Domain.Models.Post item)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(Fakebook.Posts.Domain.Models.Post[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerator<Fakebook.Posts.Domain.Models.Post> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<Fakebook.Posts.Domain.Models.Post> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(Fakebook.Posts.Domain.Models.Post item)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}