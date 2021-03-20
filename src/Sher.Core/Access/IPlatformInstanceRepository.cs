using System.Threading.Tasks;

namespace Sher.Core.Access
{
    public interface IPlatformInstanceRepository
    {
        public Task<PlatformInstance> GetPlatformInstance();
        public Task SetupInstance(PlatformInstance instance);
    }
}