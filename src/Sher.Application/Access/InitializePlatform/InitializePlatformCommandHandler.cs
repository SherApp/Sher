using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Access;
using Sher.Core.Access.Platform;
using Sher.Core.Access.Users;

namespace Sher.Application.Access.InitializePlatform
{
    public class InitializePlatformCommandHandler : ICommandHandler<InitializePlatformCommand>
    {
        private readonly IPlatformInstanceRepository _platformRepository;
        private readonly IPasswordHashingService _passwordHashingService;
        private readonly IUserRepository _userRepository;

        public InitializePlatformCommandHandler(
            IPlatformInstanceRepository platformRepository,
            IPasswordHashingService passwordHashingService,
            IUserRepository userRepository)
        {
            _platformRepository = platformRepository;
            _passwordHashingService = passwordHashingService;
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(InitializePlatformCommand request, CancellationToken cancellationToken)
        {
            var instance = await _platformRepository.GetPlatformInstance();
            if (instance is not null) return Unit.Value;

            var (adminId, adminEmailAddress, adminPassword) = request;

            instance = new PlatformInstance(new PlatformSettings(null));
            var user = await instance.RegisterUser(adminId, adminEmailAddress, adminPassword, _passwordHashingService);
            
            user.AssignRole(new UserRole(UserRole.Admin));

            await _userRepository.AddUserAsync(user);

            instance.UpdateSettings(Guid.NewGuid().ToString());
            await _platformRepository.SetupInstance(instance);

            return Unit.Value;
        }
    }
}