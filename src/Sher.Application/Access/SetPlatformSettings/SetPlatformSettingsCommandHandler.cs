using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sher.Application.Processing;
using Sher.Core.Access;

namespace Sher.Application.Access.SetPlatformSettings
{
    public class SetPlatformSettingsCommandHandler : ICommandHandler<SetPlatformSettingsCommand>
    {
        private readonly IPlatformInstanceRepository _platformInstanceRepository;

        public SetPlatformSettingsCommandHandler(IPlatformInstanceRepository platformInstanceRepository)
        {
            _platformInstanceRepository = platformInstanceRepository;
        }

        public async Task<Unit> Handle(SetPlatformSettingsCommand request, CancellationToken cancellationToken)
        {
            var instance = await _platformInstanceRepository.GetPlatformInstance();
            instance.UpdateSettings(request.SettingsDto.InvitationCode);

            return Unit.Value;
        }
    }
}