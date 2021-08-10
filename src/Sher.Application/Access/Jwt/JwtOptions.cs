namespace Sher.Application.Access.Jwt
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}