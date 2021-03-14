using AutoMapper;
using Sher.Core.Files;

namespace Sher.Application.Files.GetUploaderFiles
{
    public class FileDtoProfile : Profile
    {
        public FileDtoProfile()
        {
            CreateMap<File, FileDto>();
        }
    }
}