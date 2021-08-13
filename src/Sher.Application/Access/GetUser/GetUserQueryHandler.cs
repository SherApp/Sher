using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Sher.Application.Processing;
using Sher.Core.Access.Users;

namespace Sher.Application.Access.GetUser
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId);
            return _mapper.Map<UserDto>(user);
        }
    }
}