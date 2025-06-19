using Application.Commands;
using Application.Handlers;
using Infrastructure.Contracts;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class UpdateProductHandlerTests : IDisposable
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly UpdateProductHandler _handler;
        public UpdateProductHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new UpdateProductHandler(_mockRepository.Object);
        }
        public void Dispose()
        {
            _mockRepository.Reset();
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldUpdateProduct_WhenCalled()
        {
            // Arrange  
            var product = new Domain.Entities.Product { Id = 1, Name = "Updated Product", Price = 15.0m };
            _mockRepository.Setup(repo => repo.UpdateProduct(It.IsAny<Domain.Entities.Product>()))
                .ReturnsAsync(product);
            
            var command = new UpdateProductCommand { Id = 1, Name = "Updated Product", Price = 15.0m };
            var cancellationToken = CancellationToken.None;
            
            // Act  
            var result = await _handler.Handle(command, cancellationToken);
            
            // Assert  
            NUnit.Framework.Assert.That(result, Is.EqualTo(product.Id));
            _mockRepository.Verify(repo => repo.UpdateProduct(It.IsAny<Domain.Entities.Product>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowArgumentException_WhenInvalidData()
        {
            // Arrange  
            var invalidCommand = new UpdateProductCommand { Id = 0, Name = "", Price = -1 };
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(invalidCommand, CancellationToken.None));
        }
    }
}
