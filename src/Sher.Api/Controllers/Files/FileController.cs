using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Application.Files.DeleteFile;
using Sher.Application.Files.GetFile;
using Sher.Application.Files.GetUploaderFiles;

namespace Sher.Api.Controllers.Files
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ApiController
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<FileDto>), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersFiles([FromQuery] string requiredFileNamePart = null)
        {
            var files = await _mediator.Send(new GetUploaderFilesQuery(UserId, requiredFileNamePart));
            return Ok(files);
        }

        [HttpDelete("{fileId:guid}")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteFile(Guid fileId)
        {
            await _mediator.Send(new DeleteFileCommand(fileId, UserId));
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{fileId:guid}")]
        public async Task<IActionResult> GetFile(Guid fileId)
        {
            var file = await _mediator.Send(new GetFileQuery(fileId));
            var filePath = Path.Combine("wwwroot/u", file.UploaderId.ToString(), file.Id.ToString("N"));
            
            var stream = new FileStream(filePath, FileMode.Open);
            
            return File(stream, file.ContentType, file.FileName);
        }
    }
}