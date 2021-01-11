using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Fakebook.Posts.DataAccess.Mappers;
using Fakebook.Posts.Domain.Interfaces;
//using Fakebook.Posts.DataAccess.Models;
using Fakebook.Posts.Domain.Models;
using System.Threading;

namespace Fakebook.Posts.DataAccess.Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private readonly FakebookPostsContext _context;
        public PostsRepository(FakebookPostsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Post>> NewsfeedAsync(string email, int count)
        {
            var posts = await _context.Posts.FromSqlInterpolated($"SELECT * FROM ( SELECT *, ROW_NUMBER() OVER ( PARTITION BY UserEmail ORDER BY CreatedAt DESC ) AS RowNum FROM Post WHERE UserEmail = {email} OR UserEmail IN ( SELECT FollowedEmail FROM Follow WHERE FollowerEmail = {email} ) ) AS RecentPosts WHERE RowNum <= {count}").ToListAsync();
            return posts.Select(p => p.ToDomain());
        }
/*
SELECT *
FROM (
    SELECT *, 
    ROW_NUMBER() OVER (
        PARTITION BY UserEmail 
        ORDER BY CreatedAt DESC
    ) AS RowNum
    FROM Post
    WHERE UserEmail = @email
    OR UserEmail IN (
        SELECT FollowedEmail 
        FROM Follow 
        WHERE FollowerEmail = @email
    )
) AS RecentPosts
WHERE RowNum <= 3
*/

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
            => _context.Posts.Select(x => x.ToDomain()).AsAsyncEnumerable().GetAsyncEnumerator(cancellationToken);

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// An enumerator that can be used to iterate through the collection,
        /// where Posts do NOT contain their comments.
        /// </returns>
        public IEnumerator<Post> GetEnumerator()
            => _context.Posts.Select(x => x.ToDomain()).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator(); 

        public void Add(Post item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LikePostAsync(int postId, string userEmail)
        {
            try {
                await _context.PostLikes.AddAsync(new Models.PostLike { PostId = postId, LikerEmail = userEmail });
                await _context.SaveChangesAsync();
                return true;
            } catch {
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
            try {
                await _context.CommentLikes.AddAsync(new Models.CommentLike { CommentId = commentId, LikerEmail = userEmail });
                await _context.SaveChangesAsync();
                return true;
            } catch {
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

        IAsyncEnumerator<Domain.Models.Post> IAsyncEnumerable<Domain.Models.Post>.GetAsyncEnumerator(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        IEnumerator<Domain.Models.Post> IEnumerable<Domain.Models.Post>.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
