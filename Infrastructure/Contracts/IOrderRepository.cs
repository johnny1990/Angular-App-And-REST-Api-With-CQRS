using Domain.Entities;

namespace Infrastructure.Contracts
{
    public interface IOrderRepository
    {
        Task<int> CreateOrder(Order order, List<OrderProduct> products);
    }
}
