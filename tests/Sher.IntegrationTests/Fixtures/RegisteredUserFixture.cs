using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Sher.Api;
using Sher.Api.Controllers.Access;
using Sher.Application.Access.AuthenticateUser;
using Sher.Application.Access.GetPlatformSettings;
using Sher.Application.Access.RegisterUser;
using Sher.Application.Access.SetPlatformSettings;
using Sher.IntegrationTests.Utils;

namespace Sher.IntegrationTests.Fixtures
{
    public class RegisteredUserFixture
    {
        private HttpClient _authorizedClient;
        private readonly TestServerFactory<Startup> _factory;

        public RegisteredUserFixture(TestServerFactory<Startup> factory)
        {
            _factory = factory;
        }

        public async Task<HttpClient> GetOrCreateAuthorizedClient(
            string email = "email@example.com",
            string password = "Testing1234",
            bool reuseAnyExistingClient = true)
        {
            if (reuseAnyExistingClient && _authorizedClient is not null)
            {
                return _authorizedClient;
            }

            await _factory.Services.DispatchCommand(new SetPlatformSettingsCommand(new PlatformSettingsDto
                {InvitationCode = null}));
            await _factory.Services.DispatchCommand(new RegisterUserCommand(Guid.NewGuid(), email, password, null));

            var client = _factory.CreateClient();

            var newTokenResponse = await client.PostAsJsonAsync("/api/token/new?asCookie=false", new IssueTokenRequestModel
            {
                EmailAddress = email,
                Password = password
            });
            
            var authResult = await newTokenResponse.Content.ReadFromJsonAsync<AuthenticationResult>();
            
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {authResult!.JwtToken}");

            return _authorizedClient = client;
        }
    }
}