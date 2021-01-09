using Fakebook.Posts.DataAccess.Mappers;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Fakebook.Posts.DataAccess.Repositories
{
    public class PostsRepository : IPostsRepository {

        private readonly FakebookPostsContext _context;

        public PostsRepository(FakebookPostsContext context) {
            _context = context;
        }

        public async ValueTask<Fakebook.Posts.Domain.Models.Post> AddAsync(Fakebook.Posts.Domain.Models.Post post) {
            var postDb = post.ToDataAccess();
            await _context.Posts.AddAsync(postDb);
            await _context.SaveChangesAsync();
            return postDb.ToDomain();
        }

        public async ValueTask DeletePostAsync(int id) {
            if (await _context.Posts.FindAsync(id) is DataAccess.Models.Post post) {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            } else {
                throw new ArgumentException("Post with given id not found.", nameof(id));
            }
        }

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(Post item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Post[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerator<Post> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<Post> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Post item)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(Post item)
        {
            throw new NotImplementedException();
        }
    }
}