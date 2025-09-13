using System;
using Moq;
using Xunit;
using VantagePoint.Domain.Common;

namespace VantagePoint.Domain.Common.Tests;

public class ServiceTests {
    private sealed class TestService : Service {
        public TestService(IUnitOfWork unitOfWork)
            : base(unitOfWork) {
        }
    }

    [Fact]
    public void Ctor_WithValidUnitOfWork_SetsUnitOfWorkProperty() {
        // Arrange
        var uowMock = new Mock<IUnitOfWork>();

        // Act
        var service = new TestService(uowMock.Object);

        // Assert
        Assert.Same(uowMock.Object, service.UnitOfWork);
    }

    [Fact]
    public void Ctor_NullUnitOfWork_ThrowsArgumentNullException() {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new TestService(null!));
    }
}
