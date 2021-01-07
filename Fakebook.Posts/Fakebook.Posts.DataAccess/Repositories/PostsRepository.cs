using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fakebook.Posts.Domain.Models;
using Fakebook.Posts.Domain.Interfaces;

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
    }
}
