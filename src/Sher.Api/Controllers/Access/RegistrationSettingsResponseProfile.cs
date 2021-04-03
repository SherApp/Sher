using AutoMapper;
using Sher.Application.Access.GetPlatformSettings;

namespace Sher.Api.Controllers.Access
{
    public class RegistrationSettingsResponseProfile : Profile
    {
        public RegistrationSettingsResponseProfile()
        {
            CreateMap<PlatformSettingsDto, RegistrationSettingsResponse>()
                .ForMember(dest => dest.RequiresInvitationCode,
                    opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.InvitationCode)));
        }
    }
}