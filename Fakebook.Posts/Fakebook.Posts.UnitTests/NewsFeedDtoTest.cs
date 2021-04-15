using System.Collections.Generic;
using Fakebook.Posts.RestApi.Dtos;
using Xunit;

namespace Fakebook.Posts.UnitTests
{
    public class NewsFeedDtoTest
    {
        [Fact]
        public void NewsFeedDto_ValidatingEmails_ReturnsTrue()
        {


            //arrange
            NewsFeedDto dto = new NewsFeedDto();
            dto.Emails = new List<string> { "damion.silver@test.com" };
            List<string> mockEmailDtoList = new List<string> { "damion.silver@test.com" };
            //act
            Assert.Equal(mockEmailDtoList, dto.Emails);

        }

    }
}
