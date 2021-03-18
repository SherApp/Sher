namespace Sher.SharedKernel.Options
{
    public class JwtOptions
    {
        public string SecurityKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}