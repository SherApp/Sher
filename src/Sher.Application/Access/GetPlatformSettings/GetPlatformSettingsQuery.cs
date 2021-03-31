using MediatR;

namespace Sher.Application.Access.GetPlatformSettings
{
    public record GetPlatformSettingsQuery : IRequest<PlatformSettingsDto>;
}