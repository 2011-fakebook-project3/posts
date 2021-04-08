using System;

namespace Fakebook.Posts.RestApi.Services
{
    public class TimeService : ITimeService
    {
        public DateTime CurrentTime => DateTime.Now;
    }
}
