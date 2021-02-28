using System;
using System.IO;
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
    }
}