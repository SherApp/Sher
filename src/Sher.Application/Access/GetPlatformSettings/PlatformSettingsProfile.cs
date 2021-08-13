using AutoMapper;
using Sher.Core.Access.Platform;

namespace Sher.Application.Access.GetPlatformSettings
{
    public class PlatformSettingsProfile : Profile
    {
        public PlatformSettingsProfile()
        {
            CreateMap<PlatformSettings, PlatformSettingsDto>();
        }
    }
}