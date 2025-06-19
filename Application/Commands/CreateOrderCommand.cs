using Domain.Models;
using MediatR;

namespace Application.Commands
{
    public class CreateOrderCommand : IRequest<int>, IBaseRequest
    {
        public int UserId { get; set; }
        public List<OrderProductModel> Products { get; set; }

        // Add a constructor to fix CS1729 error  
        public CreateOrderCommand(int userId, List<OrderProductModel> products)
        {
            UserId = userId;
            Products = products;
        }
    }
 
}
