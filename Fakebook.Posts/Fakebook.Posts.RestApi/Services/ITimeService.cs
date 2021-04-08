using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fakebook.Posts.RestApi.Services
{
    public interface ITimeService
    {
        DateTime GetCurrentTime();
    }
}