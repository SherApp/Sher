using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSpecifications;

namespace Sher.Core.Files
{
    public interface IFileRepository
    {
        public Task<File> GetByIdAsync(Guid id);
        public Task<IReadOnlyList<File>> SearchAsync(ISpecification<File> criteria);
        public Task AddAsync(File file);
    }
}