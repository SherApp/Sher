using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Files.UploadFile;

namespace Sher.Api.Files
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequestModel model)
        {
            await _mediator.Send(new UploadFileCommand(model.Id,
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                model.File.FileName,
                model.File.OpenReadStream()));

            return Accepted();
        }
    }
}