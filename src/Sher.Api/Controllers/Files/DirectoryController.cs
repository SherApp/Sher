using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Files.ListDirectory;

namespace Sher.Api.Controllers.Files
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DirectoryController : ApiController
    {
        private readonly IMediator _mediator;

        public DirectoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{directoryId:guid?}")]
        public async Task<IActionResult> ListDirectory(Guid? directoryId)
        {
            var result = await _mediator.Send(new ListDirectoryQuery(directoryId, UserId));
            return Ok(result);
        }
    }
}