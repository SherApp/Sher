namespace Sher.Api.Controllers.Access
{
    public class IssueTokenRequestModel
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}