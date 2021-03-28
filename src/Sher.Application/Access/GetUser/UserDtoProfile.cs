using System.Linq;
using AutoMapper;
using Sher.Core.Access.Users;

namespace Sher.Application.Access.GetUser
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(r => r.Name)));
        }
    }
}