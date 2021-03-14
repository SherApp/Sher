using System.Threading.Tasks;

namespace Sher.Core.Base
{
    public interface IUnitOfWork
    {
        public Task CommitChangesAsync();
    }
}