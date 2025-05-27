using Application.Commands;
using Domain.Entities;
using Infrastructure.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private readonly IOrderRepository _orderRepository;

       
        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.UserId <= 0 || request.Products == null || !request.Products.Any())
                    throw new ArgumentException("Invalid order data");

                var order = new Order
                {
                    UserId = request.UserId,
                    DateAdded = DateTime.UtcNow
                };

                var orderProducts = request.Products.Select(p => new OrderProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Order = order
                }).ToList();

                return await _orderRepository.CreateOrder(order, orderProducts);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred while creating the order.", ex);
            }
        }

    }
}
