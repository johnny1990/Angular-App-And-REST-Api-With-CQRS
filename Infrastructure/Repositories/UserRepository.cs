using Domain.Context;
using Domain.Models;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly OMDbContext _context;
        private bool _disposed = false;

        public UserRepository(OMDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopSpendingUsersModel>> GetTopSpendingUsers()
        {
            var sixMonthsDate = DateTime.UtcNow.AddMonths(-6);

            var result = await _context.Users
                .AsNoTracking()
                .Join(_context.Orders.Where(o => o.DateAdded >= sixMonthsDate),
                      user => user.Id,
                      order => order.UserId,
                      (user, order) => new { user, order })
                .Join(_context.OrderProducts,
                      combined => combined.order.Id,
                      op => op.OrderId,
                      (combined, op) => new
                      {
                          combined.user.Id,
                          combined.user.Name,
                          Amount = op.Quantity * op.Price
                      })
                .GroupBy(x => new { x.Id, x.Name })
                .Select(g => new TopSpendingUsersModel
                {
                    Id = g.Key.Id,
                    Name = g.Key.Name,
                    TotalSpent = g.Sum(x => x.Amount)
                })
                .Where(x => x.TotalSpent > 1000)
                .OrderByDescending(x => x.TotalSpent)
                .Take(1000)
                .ToListAsync();
            return result;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
