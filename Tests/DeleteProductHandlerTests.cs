using Application.Commands;
using Application.Handlers;
using Infrastructure.Contracts;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class DeleteProductHandlerTests : IDisposable
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly DeleteProductHandler _handler;
        public DeleteProductHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new DeleteProductHandler(_mockRepository.Object);
        }
        public void Dispose()
        {
            _mockRepository.Reset();
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldDeleteProduct_WhenCalled()
        {
            // Arrange  
            var productId = 1;
            _mockRepository.Setup(repo => repo.DeleteProduct(productId))
                .ReturnsAsync(productId);
            var command = new DeleteProductCommand { Id = productId };
            var cancellationToken = CancellationToken.None;
            // Act  
            var result = await _handler.Handle(command, cancellationToken);
            // Assert  
            NUnit.Framework.Assert.That(result, Is.EqualTo(productId));
            _mockRepository.Verify(repo => repo.DeleteProduct(productId), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowArgumentException_WhenInvalidData()
        {
            // Arrange  
            var invalidCommand = new DeleteProductCommand { Id = 0 };
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(invalidCommand, CancellationToken.None));
        }
    }
}

