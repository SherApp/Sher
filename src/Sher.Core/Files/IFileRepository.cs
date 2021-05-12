using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSpecifications;

namespace Sher.Core.Files
{
    public interface IFileRepository
    {
        public Task<File> GetByIdAsync(Guid id);
        public Task<File> GetWithUploaderIdAsync(Guid uploaderId, Guid fileId);
        public Task<IReadOnlyList<File>> SearchAsync(ISpecification<File> criteria);
        public Task<IReadOnlyList<File>> GetWithParentDirectoryAsync(Guid directoryId);
        public Task AddAsync(File file);
    }
}