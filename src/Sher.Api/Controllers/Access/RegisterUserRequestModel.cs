using System;
using System.ComponentModel.DataAnnotations;

namespace Sher.Api.Controllers.Access
{
    public class RegisterUserRequestModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public string InvitationCode { get; set; }
    }
}