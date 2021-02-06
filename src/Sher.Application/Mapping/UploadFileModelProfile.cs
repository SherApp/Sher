using System.IO;
using AutoMapper;
using Sher.Application.Models;
using Sher.Core.Commands;

namespace Sher.Application.Mapping
{
    public class UploadFileModelProfile : Profile
    {
        public UploadFileModelProfile()
        {
            CreateMap<UploadFileModel, FileUploadCommand>()
                .ForCtorParam("Slug", cfg => cfg.MapFrom(src => Path.Combine(src.Id.ToString(), src.File.FileName)))
                .ForCtorParam("OriginalFileName", cfg => cfg.MapFrom(src => src.File.FileName))
                .ForCtorParam("FileStream", cfg => cfg.MapFrom(src => src.File.Stream));
        }
    }
}