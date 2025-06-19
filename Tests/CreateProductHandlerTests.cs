using Application.Commands;
using Application.Handlers;
using Infrastructure.Contracts;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class CreateProductHandlerTests : IDisposable
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly CreateProductHandler _handler;

        public CreateProductHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new CreateProductHandler(_mockRepository.Object);
        }
        public void Dispose()
        {
            _mockRepository.Reset();
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldCreateProduct_WhenCalled()
        {
            // Arrange  
            var product = new Domain.Entities.Product { Id = 1, Name = "Test Product", Price = 10.0m };
            _mockRepository.Setup(repo => repo.CreateProduct(It.IsAny<Domain.Entities.Product>()))
                .ReturnsAsync(product);

            var command = new CreateProductCommand { Name = "Test Product", Price = 10.0m };
            var cancellationToken = CancellationToken.None;

            // Act  
            var result = await _handler.Handle(command, cancellationToken);

            // Assert  
            NUnit.Framework.Assert.That(result, Is.EqualTo(product.Id));
            _mockRepository.Verify(repo => repo.CreateProduct(It.IsAny<Domain.Entities.Product>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowArgumentException_WhenInvalidData()
        {
            // Arrange  
            var invalidCommand = new CreateProductCommand { Name = "", Price = -1 };
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(invalidCommand, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowApplicationException_WhenRepositoryThrowsException()
        {
            // Arrange  
            _mockRepository.Setup(repo => repo.CreateProduct(It.IsAny<Domain.Entities.Product>()))
                .ThrowsAsync(new Exception("Database error"));
            var command = new CreateProductCommand { Name = "Test Product", Price = 10.0m };
            var cancellationToken = CancellationToken.None;
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ApplicationException>(async () => await _handler.Handle(command, cancellationToken));
        }
    }
}
