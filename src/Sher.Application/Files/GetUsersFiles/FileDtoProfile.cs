using AutoMapper;
using Sher.Core.Files;

namespace Sher.Application.Files.GetUsersFiles
{
    public class FileDtoProfile : Profile
    {
        public FileDtoProfile()
        {
            CreateMap<File, FileDto>();
        }
    }
}