using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Files.DeleteFile;
using Sher.Application.Files.GetUploaderFiles;
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
        [ProducesResponseType((int) HttpStatusCode.Accepted)]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequestModel model)
        {
            await _mediator.Send(new UploadFileCommand(model.Id,
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                model.File.FileName,
                model.File.OpenReadStream()));

            return Accepted();
        }
        
        [HttpGet]
        [ProducesResponseType(typeof(List<FileDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersFiles([FromQuery] string requiredFileNamePart = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var files = await _mediator.Send(new GetUploaderFilesQuery(userId, requiredFileNamePart));
            return Ok(files);
        }

        [HttpDelete("{fileId:guid}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mediator.Send(new DeleteFileCommand(fileId, userId));
            return Ok();
        }
    }
}