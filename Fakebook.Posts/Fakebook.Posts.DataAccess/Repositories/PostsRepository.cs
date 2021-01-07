﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.Domain.Interfaces;
using System.Threading;
using System.Collections;
using Fakebook.Posts.DataAccess.Mappers;

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

        /// <summary>
        ///  Returns an enumerator that iterates asynchronously through the collection.
        /// </summary>
        /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
        /// <returns>
        /// An enumerator that can be used to iterate asynchronously through the collection, 
        /// where Posts do NOT contain their comments 
        /// </returns>
        public IAsyncEnumerator<Post> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            => _context.Posts.Select(x => x.ToDomain(false)).AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection,
        /// where Posts do NOT contain their comments.
        /// </returns>
        public IEnumerator<Post> GetEnumerator()
            => _context.Posts.Select(x => x.ToDomain(false)).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 

    }
}
