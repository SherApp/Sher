using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Api.Models;
using Sher.Application.Commands;

namespace Sher.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FileController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile([FromForm] UploadFileRequestModel model)
        {
            await _mediator.Send(_mapper.Map<FileUploadCommand>(model));
            return Ok();
        }
    }
}