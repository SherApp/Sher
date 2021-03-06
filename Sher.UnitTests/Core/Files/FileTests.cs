using System;
using Sher.Core.Files;
using Xunit;
using File = Sher.Core.Files.File;

namespace Sher.UnitTests.Core.Files
{
    public class FileTests
    {
        [Fact]
        public void ValidFile_Delete_SetsIsRemoved()
        {
            var file = new File(Guid.NewGuid(), "", "", 1);
            file.Delete();
            
            Assert.True(file.IsDeleted);
        }

        [Fact]
        public void ValidFile_Delete_DispatchesDomainEvent()
        {
            var file = new File(Guid.NewGuid(), "", "", 1);
            file.Delete();

            Assert.Contains(file.DomainEvents, e =>
                e is FileDeletedEvent ev && ev.FileId == file.Id && ev.FileName == file.FileName
            );
        }
    }
}