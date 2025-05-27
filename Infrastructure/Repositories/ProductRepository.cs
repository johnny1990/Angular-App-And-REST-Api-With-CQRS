using Domain.Context;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IDisposable
    {
        private readonly OMDbContext _context;
        private bool _disposed = false;

        public ProductRepository(OMDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();
            return products; 
        }


        public async Task<Product> GetProductById(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<Product> CreateProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<int> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return 0;
            }
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        #region Report 
        public async Task<List<TopSellingProductReportModel>> GetBestSellingProductsLast30DaysReport()
        {
            var last30daysDate = DateTime.UtcNow.AddDays(-30);

            var result = await _context.OrderProducts
                .AsNoTracking()
                .Where(op => op.Order.DateAdded >= last30daysDate)
                .GroupBy(op => new { op.ProductId, op.Product.Name })
                .Select(g => new TopSellingProductReportModel
                {
                    ProductName = g.Key.Name,
                    UnitsSold = g.Sum(op => op.Quantity),
                    TotalSales = g.Sum(op => op.Quantity * op.Price)
                })
                .OrderByDescending(r => r.UnitsSold)
                .ToListAsync();

            return result;

        }
        #endregion

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
