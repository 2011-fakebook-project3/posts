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
    public class PostsRepository : IPostsRepository
    {

        private readonly FakebookPostsContext _context;
        
        public PostsRepository(FakebookPostsContext context)
        {
            _context = context;
        }

        public async ValueTask<Fakebook.Posts.Domain.Models.Post> AddAsync(Fakebook.Posts.Domain.Models.Post post)
        {
            var postDb = post.ToDataAccess();
            await _context.Posts.AddAsync(postDb);
            await _context.SaveChangesAsync();
            return postDb.ToDomain();
        }

        public int Count => throw new NotImplementedException();

        public async ValueTask<Domain.Models.Post> GetAsync(int id) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the content property of the given post in the database. Db column remains unchanged if property value is null.
        /// </summary>
        /// <param name="post">The domain post model containing the updated property values.</param>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        public async ValueTask UpdateAsync(Domain.Models.Post post) {
            if (await _context.Posts.FindAsync(post.Id) is DataAccess.Models.Post current) {
                current.Content = post.Content ?? current.Content;
                
                await _context.SaveChangesAsync(); // Will throw DbUpdateException if a database constraint is violated.
            }

            throw new ArgumentException("Post with given Id not found.", nameof(post.Id));

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
