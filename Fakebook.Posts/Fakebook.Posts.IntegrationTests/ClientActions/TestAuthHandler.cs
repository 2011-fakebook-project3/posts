using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fakebook.Posts.IntegrationTests.Controllers
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, "test.user@email.com") };
            ClaimsIdentity identity = new(claims, "Test");
            ClaimsPrincipal principal = new(identity);
            AuthenticationTicket ticket = new(principal, "Test");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
