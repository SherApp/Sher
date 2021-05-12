using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSpecifications;
using Sher.Core.Files;

namespace Sher.UnitTests.Utils.Data
{
    public class InMemoryFileRepository : IFileRepository
    {
        private readonly List<File> _files = new();

        public Task<File> GetByIdAsync(Guid id)
        {
            return Task.FromResult(_files.FirstOrDefault(f => f.Id == id));
        }

        public Task<File> GetWithUploaderIdAsync(Guid uploaderId, Guid fileId)
        {
            return Task.FromResult(_files.FirstOrDefault(f => f.UploaderId == uploaderId && f.Id == fileId));
        }

        public Task<IReadOnlyList<File>> SearchAsync(ISpecification<File> criteria)
        {
            return Task.FromResult(
                (IReadOnlyList<File>) _files.Where(criteria.Expression.Compile()).ToList().AsReadOnly());
        }

        public Task<IReadOnlyList<File>> GetWithParentDirectoryAsync(Guid directoryId)
        {
            return Task.FromResult(
                (IReadOnlyList<File>) _files.Where(f => f.DirectoryId == directoryId).ToList().AsReadOnly());
        }

        public Task AddAsync(File file)
        {
            _files.Add(file);
            return Task.CompletedTask;
        }
    }
}