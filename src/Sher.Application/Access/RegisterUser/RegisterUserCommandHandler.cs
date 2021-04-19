using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Access;
using Sher.Core.Access.Platform;
using Sher.Core.Access.Users;

namespace Sher.Application.Access.RegisterUser
{
    public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
    {
        private readonly IPlatformInstanceRepository _platformInstanceRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(
            IPlatformInstanceRepository platformInstanceRepository,
            IUserRepository userRepository,
            IPasswordHashingService passwordHashingService)
        {
            _platformInstanceRepository = platformInstanceRepository;
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var instance = await _platformInstanceRepository.GetPlatformInstance();

            var user = await instance.RegisterUser(
                request.UserId, 
                request.EmailAddress, 
                request.Password,
                _passwordHashingService, 
                request.InvitationCode);

            await _userRepository.AddUserAsync(user);

            return Unit.Value;
        }
    }
}