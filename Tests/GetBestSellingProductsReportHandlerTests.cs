using Application.Handlers;
using Application.Queries;
using Domain.Models;
using Infrastructure.Contracts;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class GetBestSellingProductsReportHandlerTests : IDisposable
    {
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly GetBestSellingProductsReportHandler _handler;
        public GetBestSellingProductsReportHandlerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            _handler = new GetBestSellingProductsReportHandler(_mockRepository.Object);
        }
        public void Dispose()
        {
            _mockRepository.Reset();
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldReturnBestSellingProducts_WhenCalled()
        {
            // Arrange  
            var products = new List<TopSellingProductReportModel>
            {
                new TopSellingProductReportModel { ProductName = "Product A", UnitsSold = 100, TotalSales = 1000m },
                new TopSellingProductReportModel { ProductName = "Product B", UnitsSold = 200, TotalSales = 2000m }
            };
            _mockRepository.Setup(repo => repo.GetBestSellingProductsLast30DaysReport())
                .ReturnsAsync(products);
            // Act  
            var result = await _handler.Handle(new GetBestSellingProductsReportQuery(), CancellationToken.None);
            // Assert  
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(2));
            _mockRepository.Verify(repo => repo.GetBestSellingProductsLast30DaysReport(), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowApplicationException_WhenRepositoryThrowsException()
        {
            // Arrange  
            _mockRepository.Setup(repo => repo.GetBestSellingProductsLast30DaysReport())
                .ThrowsAsync(new Exception("Database error"));
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ApplicationException>(async () => await _handler.Handle(new GetBestSellingProductsReportQuery(), CancellationToken.None));
        }

        [Test]
        public void Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            // Arrange  
            GetBestSellingProductsReportQuery nullRequest = null;
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(nullRequest, CancellationToken.None));
        }
    }
}
