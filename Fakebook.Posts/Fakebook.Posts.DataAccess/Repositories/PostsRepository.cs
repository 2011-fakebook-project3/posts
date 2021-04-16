using Fakebook.Posts.DataAccess.Mappers;
using Fakebook.Posts.Domain.Interfaces;
using Fakebook.Posts.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<Post>> NewsfeedAsync(ICollection<string> followingEmails, int maxPost = 50)
        {
            if (followingEmails != null)
            {
                var recentPosts = await _context.Posts
                    .Include(p => p.PostLikes)
                    .Include(p => p.Comments)
                    .ThenInclude(c => c.CommentLikes)
                    .Where(u => followingEmails.Contains(u.UserEmail))
                    .OrderByDescending(t => t.CreatedAt)
                    .Take(maxPost)
                    .ToListAsync();

                return recentPosts.Select(p => p.ToDomain());
            }
            else
            {
                throw new ArgumentNullException(nameof(followingEmails), "You can not have null email list.");
            }
        }


        public async ValueTask<Post> AddAsync(Post post)
        {
            var postDb = post.ToDataAccess();
            await _context.Posts.AddAsync(postDb);
            await _context.SaveChangesAsync();
            return postDb.ToDomain();
        }

        public async ValueTask<Post> GetAsync(int postId)
        {
            var post = await _context.Posts.FirstAsync(p => p.Id == postId);
            return post.ToDomain();
        }

        public async ValueTask<Comment> AddCommentAsync(Comment comment)
        {
            if (await _context.Posts.FirstOrDefaultAsync(p => p.Id == comment.PostId) is Models.Post post)
            {
                var commentDb = comment.ToDataAccess(post.Id);
                await _context.Comments.AddAsync(commentDb);
                await _context.SaveChangesAsync();
                return commentDb.ToDomain(null);
            }
            else
            {
                throw new ArgumentException($"Post { comment.PostId } not found.", nameof(comment));
            }
        }

        public async ValueTask DeletePostAsync(int id)
        {
            if (await _context.Posts.FindAsync(id) is Models.Post post)
            {
                _context.Remove(post);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Post with given id not found.", nameof(id));
            }
        }

        public async ValueTask DeleteCommentAsync(int id)
        {
            if (await _context.Comments.FindAsync(id) is Models.Comment comment)
            {
                _context.Remove(comment);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Comment with given id not found.", nameof(id));
            }
        }

        /// <summary>
        /// Updates the content property of the given post in the database. Db column remains unchanged if property value is null.
        /// </summary>
        /// <param name="post">The domain post model containing the updated property values.</param>
        /// <exception cref="ArgumentException">ArgumentException</exception>
        public async ValueTask UpdateAsync(Post post)
        {
            if (await _context.Posts.FindAsync(post.Id) is Models.Post current)
            {
                current.Content = post.Content ?? current.Content;

                await _context.SaveChangesAsync(); // Will throw DbUpdateException if a database constraint is violated.
            }
            else
            {
                throw new ArgumentException("Post with given Id not found.", nameof(post));
            }
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
            => _context.Posts.Include(p => p.PostLikes).Select(x => x.ToDomain()).AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection,
        /// where Posts do NOT contain their comments.
        /// </returns>
        public IEnumerator<Post> GetEnumerator()
            => _context.Posts.Include(p => p.PostLikes).Select(x => x.ToDomain()).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public async Task<bool> LikePostAsync(int postId, string userEmail)
        {
            try
            {
                await _context.PostLikes.AddAsync(new Models.PostLike { PostId = postId, LikerEmail = userEmail });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnlikePostAsync(int postId, string userEmail)
        {
            if (await _context.PostLikes.FindAsync(userEmail, postId) is Models.PostLike like)
            {
                _context.PostLikes.Remove(like);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> LikeCommentAsync(int commentId, string userEmail)
        {
            try
            {
                await _context.CommentLikes.AddAsync(new Models.CommentLike { CommentId = commentId, LikerEmail = userEmail });
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UnlikeCommentAsync(int commentId, string userEmail)
        {
            if (await _context.CommentLikes.FindAsync(userEmail, commentId) is Models.CommentLike like)
            {
                _context.CommentLikes.Remove(like);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
