using Domain.Models;
using MediatR;

namespace Application.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public int UserId { get; set; }
        public List<OrderProductModel> Products { get; set; } = new();
    }

 
}
