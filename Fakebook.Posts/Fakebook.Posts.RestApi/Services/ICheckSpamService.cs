using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.RestApi.Services
{
    public interface ICheckSpamService
    {
        Task<bool> IsPostNotSpam(Post userPost);
    }
}
