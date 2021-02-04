using AutoMapper;
using Microsoft.AspNetCore.Http;
using Sher.Api.Models;
using Sher.Application.Models;

namespace Sher.Api.Mapping
{
    public class FileModelProfile : Profile
    {
        public FileModelProfile()
        {
            CreateMap<IFormFile, FileModel>()
                .ForMember(dest => dest.Stream, cfg => cfg.MapFrom(src => src.OpenReadStream()));

            CreateMap<UploadFileRequestModel, UploadFileModel>();
        }
    }
}