using System;

namespace Fakebook.Posts.RestApi.Services
{
    public interface ITimeService
    {
        DateTime CurrentTime { get; }
    }
}
