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
    public class CreateOrderHandlerTests : IDisposable
    {

        private readonly Mock<IOrderRepository> _mockRepository;
        private readonly CreateOrderHandler _handler;

        public CreateOrderHandlerTests()
        {
            _mockRepository = new Mock<IOrderRepository>();
            _handler = new CreateOrderHandler(_mockRepository.Object);
        }
        public void Dispose()
        {
            _mockRepository.Reset();
            _handler.Equals(null);
        }

        //[Test]
        //public async Task Handle_ShouldCreateOrder_WhenCalled()
        //{
        //    // Arrange  
        //    var order = new Domain.Entities.Order { Id = 1, UserId = 1, DateAdded = DateTime.UtcNow };
        //    var orderProducts = new List<Domain.Entities.OrderProductModel>(); // Provide the required 'products' argument  
        //    _mockRepository.Setup(repo => repo.CreateOrder(It.IsAny<Domain.Entities.Order>(), It.IsAny<List<Domain.Entities.OrderProduct>>()))
        //        .ReturnsAsync(order.Id);

        //    // Act  
        //    var result = await _handler.Handle(new Application.Commands.CreateOrderCommand(order.UserId, orderProducts), default);

        //    // Assert  
        //    NUnit.Framework.Assert.That(result, Is.Not.Null);
        //    NUnit.Framework.Assert.That(result, Is.EqualTo(order.Id));
        //    _mockRepository.Verify(repo => repo.CreateOrder(It.IsAny<Domain.Entities.Order>(), It.IsAny<List<Domain.Entities.OrderProduct>>()), Times.Once);
        //}

        //[Test]
        //public void Handle_ShouldThrowArgumentException_WhenInvalidData()
        //{
        //    // Arrange  
        //    var invalidOrder = new Application.Commands.CreateOrderCommand(0, null);
        //    // Act & Assert  
        //    NUnit.Framework.Assert.ThrowsAsync<ArgumentException>(async () => await _handler.Handle(invalidOrder, default));
        //}

        //[Test]
        //public void Handle_ShouldThrowApplicationException_WhenRepositoryThrowsException()
        //{
        //    // Arrange  
        //    var order = new Domain.Entities.Order { Id = 1, UserId = 1, DateAdded = DateTime.UtcNow };
        //    var orderProducts = new List<Domain.Entities.OrderProduct>(); // Correct type used here  
        //    _mockRepository.Setup(repo => repo.CreateOrder(It.IsAny<Domain.Entities.Order>(), It.IsAny<List<Domain.Entities.OrderProduct>>()))
        //        .ThrowsAsync(new Exception("Database error"));

        //    // Act & Assert  
        //    NUnit.Framework.Assert.ThrowsAsync<ApplicationException>(async () => await _handler.Handle(new Application.Commands.CreateOrderCommand(order.UserId, orderProducts), default));
        //}

    }
}
