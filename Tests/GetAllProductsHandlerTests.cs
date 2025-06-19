using Application.Handlers;
using Application.Queries;
using Domain.Entities;
using Infrastructure.Contracts;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class GetAllProductsHandlerTests : IDisposable
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly GetAllProductsHandler _handler;

        public GetAllProductsHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new GetAllProductsHandler(_mockRepository.Object);
        }

        public void Dispose()
        {
            _mockRepository.Reset();
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldReturnAllProducts_WhenCalled()
        {
            // Arrange  
            var products = new List<Product>
                {
                    new Product { Id = 1, Name = "Product 1", Price = 10.0m },
                    new Product { Id = 2, Name = "Product 2", Price = 20.0m }
                };
            _mockRepository.Setup(repo => repo.GetAllProducts()).ReturnsAsync(products);
            // Act  
            var result = await _handler.Handle(new GetAllProductsQuery(), default);
            // Assert  
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(2));
            _mockRepository.Verify(repo => repo.GetAllProducts(), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            // Arrange  
            GetAllProductsQuery request = null;
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(request, default));
        }

        [Test]
        public void Handle_ShouldThrowApplicationException_WhenRepositoryThrowsException()
        {
            // Arrange  
            _mockRepository.Setup(repo => repo.GetAllProducts()).ThrowsAsync(new Exception("Database error"));
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ApplicationException>(async () => await _handler.Handle(new GetAllProductsQuery(), default));
        }
    }
}
