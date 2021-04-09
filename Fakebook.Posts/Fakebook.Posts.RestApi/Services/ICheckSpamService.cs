using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fakebook.Posts.Domain.Models;

namespace Fakebook.Posts.RestApi.Services
{
    public interface ICheckSpamService
    {
        Task<bool> CheckPostSpam(Post userPost);
    }
}
