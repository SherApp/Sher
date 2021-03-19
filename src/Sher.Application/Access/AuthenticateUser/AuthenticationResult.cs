namespace Sher.Application.Access.AuthenticateUser
{
    public class AuthenticationResult
    {
        public string JwtToken { get; init; }
        public string RefreshToken { get; init; }
    }
}