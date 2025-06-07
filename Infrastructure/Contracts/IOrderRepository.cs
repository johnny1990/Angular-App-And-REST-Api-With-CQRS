using Domain.Entities;

namespace Infrastructure.Contracts
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<int> CreateOrder(Order order, List<OrderProduct> products);
    }
}
