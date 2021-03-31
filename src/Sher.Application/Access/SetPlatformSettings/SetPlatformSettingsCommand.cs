using Sher.Application.Access.GetPlatformSettings;
using Sher.Application.Processing;

namespace Sher.Application.Access.SetPlatformSettings
{
    public record SetPlatformSettingsCommand(PlatformSettingsDto SettingsDto) : ICommand;
}