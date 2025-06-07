using Application.Queries;
using Domain.Entities;
using Infrastructure.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrdersQuery, List<Order>>
    {
        private readonly IOrderRepository _repository;

        public GetAllOrdersHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "The request cannot be null.");
                var orders = await _repository.GetAllOrders();
                return orders.ToList();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new ApplicationException("An error occurred while retrieving the order list.", ex);
            }
        }
    }
}
