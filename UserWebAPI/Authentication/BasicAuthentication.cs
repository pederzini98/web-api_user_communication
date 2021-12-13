using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace UserWebAPI.Authentication
{
    public class BasicAuthentication : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly string auth_Pass;

        public BasicAuthentication(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            auth_Pass = configuration.GetConnectionString("AUTHENTICATION");
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string keyValue = null;

            if (!Request.Headers.ContainsKey("Authentication"))
                return AuthenticateResult.Fail("Please Authenticate");

            try
            {
                foreach (var item in Request.Headers)
                {
                    if (item.Key == "authentication")
                    {
                        keyValue = item.Value;
                        break;
                    }

                }
                var bytes = Convert.FromBase64String(keyValue);
                var credential = Encoding.UTF8.GetString(bytes);

                var claims = new[] { new Claim(ClaimTypes.Name, "Beta User") };
                var claimsIdentity = new ClaimsIdentity(claims, "Beta User");

                var identity = new ClaimsIdentity(claimsIdentity);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, "Beta user");

                var authorization = auth_Pass;
                if (credential == authorization)
                {
                    return AuthenticateResult.Success(ticket);
                }
                else
                {
                    return AuthenticateResult.Fail("Please authenticate");

                }

            }
            catch (Exception)
            {

                return AuthenticateResult.Fail("Please authenticate");
            }
        }
    }
}
