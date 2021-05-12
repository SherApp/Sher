using System;
using Sher.Core.Base;
using Sher.Core.Files.Directories;
using Sher.UnitTests.Builders;
using Xunit;

namespace Sher.UnitTests.Core.Files
{
    public class DirectoryTests
    {
        [Fact]
        public void Delete_NonRootDirectory_SetsIsDeletedAndAddsDomainEvent()
        {
            // Arrange
            var directory = new DirectoryBuilder()
                .WithParentDirectoryId(Guid.NewGuid())
                .Build();

            // Act
            directory.Delete();
            
            // Assert
            Assert.True(directory.IsDeleted);
            Assert.Contains(directory.DomainEvents,
                e => e is DirectoryDeletedEvent deletedEvent && deletedEvent.DirectoryId == directory.Id);
        }

        [Fact]
        public void Delete_RootDirectory_ThrowsExceptionAndDoesNotSetIsDeleted()
        {
            // Arrange
            var directory = new DirectoryBuilder().Build();

            // Act
            var action = new Action(() => directory.Delete());

            // Assert
            Assert.Throws<BusinessRuleViolationException>(action);
            Assert.False(directory.IsDeleted);
        }
    }
}