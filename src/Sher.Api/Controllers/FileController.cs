using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sher.Api.Models;
using Sher.Application.Interfaces;
using Sher.Application.Models;

namespace Sher.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FileController(IFileService fileService, IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult UploadFile([FromForm] UploadFileRequestModel model)
        {
            _fileService.UploadFile(_mapper.Map<UploadFileModel>(model));
            return Ok();
        }
    }
}