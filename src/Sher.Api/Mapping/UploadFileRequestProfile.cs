using System.IO;
using AutoMapper;
using Sher.Api.Models;
using Sher.Application.Commands;

namespace Sher.Api.Mapping
{
    public class UploadFileRequestProfile : Profile
    {
        public UploadFileRequestProfile()
        {
            CreateMap<UploadFileRequestModel, FileUploadCommand>()
                .ForCtorParam("Slug", cfg => cfg.MapFrom(src => Path.Combine(src.Id.ToString(), src.File.FileName)))
                .ForCtorParam("OriginalFileName", cfg => cfg.MapFrom(src => src.File.FileName))
                .ForCtorParam("FileStream", cfg => cfg.MapFrom(src => src.File.OpenReadStream()))
                .ForAllOtherMembers(cfg => cfg.Ignore());
        }
    }
}