using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using Sher.Core.Base;
using Sher.Infrastructure;
using Xunit;

namespace Sher.UnitTests.Infrastructure
{
    public class MediatRExtensionsTests
    {
        [Fact]
        public void PublishDomainEvents_EntityWithDomainEvent_PublishesDomainEvent()
        {
            // Arrange
            var dbContext = new FakeDbContext(new DbContextOptionsBuilder().UseInMemoryDatabase("test").Options);

            var entity = new StubEntity();
            dbContext.Set<StubEntity>().Add(entity);

            var mediatRMock = new Mock<IMediator>();
            
            // Act
            mediatRMock.Object.PublishDomainEvents(dbContext);

            // Assert
            mediatRMock.Verify(x => x.Publish(entity.DomainEvents[0], default), Times.Once);
        }

        private class FakeDbContext : DbContext
        {
            public DbSet<StubEntity> Entities { get; private set; }

            public FakeDbContext(DbContextOptions options) : base(options)
            {
            }
        }

        private class DummyDomainEvent : BaseDomainEvent
        {
            
        }

        private class StubEntity : BaseEntity
        {
            public StubEntity() : base(Guid.NewGuid())
            {
                AddDomainEvent(new DummyDomainEvent());
            }
        }
    }
}