using Domain.Models;

namespace Infrastructure.Contracts
{
    public interface IUserRepository
    {
        Task<List<TopSpendingUsersModel>> GetTopSpendingUsers();
    }
}
