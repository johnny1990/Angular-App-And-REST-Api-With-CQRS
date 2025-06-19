using Application.Handlers;
using Application.Queries;
using Infrastructure.Contracts;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class GetAllOrdersHandlerTests : IDisposable
    {
        private readonly Mock<IOrderRepository> _mock;
        private readonly GetAllOrdersHandler _handler;

        public GetAllOrdersHandlerTests()
        {
            _mock = new Mock<IOrderRepository>();
            _handler = new GetAllOrdersHandler(_mock.Object);
        }

        public void Dispose()
        {
            _mock.Reset();
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldReturnAllOrders_WhenCalled()
        {
            // Arrange  
            var orders = new List<Domain.Entities.Order>
            {
                new Domain.Entities.Order { Id = 1, UserId = 1, DateAdded = DateTime.UtcNow },
                new Domain.Entities.Order { Id = 2, UserId = 2, DateAdded = DateTime.UtcNow }
            };
            _mock.Setup(repo => repo.GetAllOrders()).ReturnsAsync(orders);

            // Act  
            var result = await _handler.Handle(new Application.Queries.GetAllOrdersQuery(), default);

            // Assert  
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(2));
            _mock.Verify(repo => repo.GetAllOrders(), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            // Arrange  
            GetAllOrdersQuery request = null;
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(request, default));
        }

        [Test]
        public void Handle_ShouldThrowApplicationException_WhenRepositoryThrowsException()
        {
            // Arrange  
            _mock.Setup(repo => repo.GetAllOrders()).ThrowsAsync(new Exception("Database error"));
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ApplicationException>(async () => await _handler.Handle(new Application.Queries.GetAllOrdersQuery(), default));
        }
    }
}
