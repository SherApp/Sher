using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Sher.Core.Access;

namespace Sher.Application.Access.GetPlatformSettings
{
    public class GetPlatformSettingsQueryHandler : IRequestHandler<GetPlatformSettingsQuery, PlatformSettingsDto>
    {
        private readonly IPlatformInstanceRepository _platformInstanceRepository;
        private readonly IMapper _mapper;

        public GetPlatformSettingsQueryHandler(
            IPlatformInstanceRepository platformInstanceRepository,
            IMapper mapper)
        {
            _platformInstanceRepository = platformInstanceRepository;
            _mapper = mapper;
        }

        public async Task<PlatformSettingsDto> Handle(GetPlatformSettingsQuery request, CancellationToken cancellationToken)
        {
            var instance = await _platformInstanceRepository.GetPlatformInstance();
            return _mapper.Map<PlatformSettingsDto>(instance.Settings);
        }
    }
}