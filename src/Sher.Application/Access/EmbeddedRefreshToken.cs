using System;

namespace Sher.Application.Access
{
    public class EmbeddedRefreshToken
    {
        public Guid ClientId { get; set; }
        public string Token { get; set; }

        public EmbeddedRefreshToken(Guid clientId, string token)
        {
            ClientId = clientId;
            Token = token;
        }

        public static EmbeddedRefreshToken Parse(string s)
        {
            var parts = s.Split('.');
            return new EmbeddedRefreshToken(Guid.Parse(parts[0]), parts[1]);
        }

        public override string ToString()
        {
            return $"{ClientId}.{Token}";
        }
    }
}