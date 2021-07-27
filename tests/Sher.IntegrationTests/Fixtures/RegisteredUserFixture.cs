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
using Xunit;

namespace Sher.IntegrationTests.Fixtures
{
    public class RegisteredUserFixture : IClassFixture<TestServerFactory<Startup>>
    {
        private HttpClient _authorizedClient;
        public TestServerFactory<Startup> TestServerFactory { get; }

        public RegisteredUserFixture()
        {
            TestServerFactory = new TestServerFactory<Startup>();
        }

        public async Task<HttpClient> GetOrCreateAuthorizedClient(string email = "email@example.com", string password = "Testing1234")
        {
            if (_authorizedClient is not null)
            {
                return _authorizedClient;
            }

            await TestServerFactory.Services.DispatchCommand(new SetPlatformSettingsCommand(new PlatformSettingsDto
                {InvitationCode = null}));
            await TestServerFactory.Services.DispatchCommand(new RegisterUserCommand(Guid.NewGuid(), email, password, null));

            var client = TestServerFactory.CreateClient();

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