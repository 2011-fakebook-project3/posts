using Fakebook.Posts.RestApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Fakebook.Posts.UnitTests.Services
{
    public class TimeServiceTests
    {
        [Fact]
        public void TimeService_Constructor_Pass()
        {
            // arrange

            // act
            TimeService timeService = new TimeService();

            // assert
        }

        [Fact]
        public void TimeService_GetTime_ApproximatelyCurrentTime()
        {
            // arrange
            TimeService timeService = new TimeService();

            // act
            DateTime currentTime = timeService.GetCurrentTime();

            // assert
            Assert.True((DateTime.Now - currentTime) < TimeSpan.FromMilliseconds(1));
        }
    }
}