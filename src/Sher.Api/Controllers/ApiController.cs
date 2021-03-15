using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Sher.Api.Controllers
{
    public abstract class ApiController : ControllerBase
    {
        protected Guid UserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}