using System.Threading.Tasks;

namespace Sher.Core.Access.Platform
{
    public interface IPlatformInstanceRepository
    {
        public Task<PlatformInstance> GetPlatformInstance();
        public Task SetupInstance(PlatformInstance instance);
    }
}