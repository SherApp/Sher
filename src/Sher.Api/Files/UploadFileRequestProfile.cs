using System.IO;
using AutoMapper;
using Sher.Application.Files.UploadFile;

namespace Sher.Api.Files
{
    public class UploadFileRequestProfile : Profile
    {
        public UploadFileRequestProfile()
        {
            CreateMap<UploadFileRequestModel, UploadFileCommand>()
                .ForCtorParam("Slug", cfg => cfg.MapFrom(src => Path.Combine(src.Id.ToString(), src.File.FileName)))
                .ForCtorParam("OriginalFileName", cfg => cfg.MapFrom(src => src.File.FileName))
                .ForCtorParam("FileStream", cfg => cfg.MapFrom(src => src.File.OpenReadStream()))
                .ForAllOtherMembers(cfg => cfg.Ignore());
        }
    }
}