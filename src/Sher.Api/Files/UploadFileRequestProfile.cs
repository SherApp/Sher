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
                .ForCtorParam("OriginalFileName", cfg => cfg.MapFrom(src => src.File.FileName))
                .ForCtorParam("FileStream", cfg => cfg.MapFrom(src => src.File.OpenReadStream()))
                .ForAllOtherMembers(cfg => cfg.Ignore());
        }
    }
}