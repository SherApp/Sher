using System;
using System.IO;
using Sher.Core.Files;
using Xunit;
using File = Sher.Core.Files.File;

namespace Sher.UnitTests.Core.Files
{
    public class FileTests
    {
        [Fact]
        public void ValidFile_GetSlug_ReturnsValidSlug()
        {
            var id = Guid.NewGuid();
            const string originalFileName = "file.jpg";

            var file = new File(id, "", originalFileName, 1);

            var expectedSlug = Path.Join(id.ToString(), originalFileName);
            Assert.Equal(expectedSlug, file.Slug);
        }

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
                e is FileDeletedEvent ev && ev.FileId == file.Id && ev.FileSlug == file.Slug
            );
        }
    }
}