using Application.Handlers;
using Application.Queries;
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
    public class GetProductByIdHandlerTests : IDisposable
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly GetProductByIdHandler _handler;
        public GetProductByIdHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new GetProductByIdHandler(_mockRepository.Object);
        }
        public void Dispose()
        {
            _mockRepository.Reset();
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldReturnProduct_WhenCalledWithValidId()
        {
            // Arrange  
            var productId = 1;
            var product = new Domain.Entities.Product { Id = productId, Name = "Test Product", Price = 10.0m };
            _mockRepository.Setup(repo => repo.GetProductById(productId))
                .ReturnsAsync(product);
            
            // Act  
            var result = await _handler.Handle(new GetProductByIdQuery(productId), CancellationToken.None);
            
            // Assert  
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Id, Is.EqualTo(productId));
            _mockRepository.Verify(repo => repo.GetProductById(productId), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            // Arrange  
            GetProductByIdQuery nullRequest = null;
            
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(nullRequest, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowArgumentException_WhenInvalidId()
        {
            // Arrange  
            var invalidId = 0;
            var request = new GetProductByIdQuery(invalidId);
            
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(request, CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowKeyNotFoundException_WhenProductNotFound()
        {
            // Arrange  
            var productId = 999; // Assuming this ID does not exist
            _mockRepository.Setup(repo => repo.GetProductById(productId))
                .ReturnsAsync((Domain.Entities.Product)null);
            
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<KeyNotFoundException>(async () => await _handler.Handle(new GetProductByIdQuery(productId), CancellationToken.None));
        }

    }
}
