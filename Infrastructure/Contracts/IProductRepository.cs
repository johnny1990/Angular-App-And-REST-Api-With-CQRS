using Domain.Entities;
using Domain.Models;

namespace Infrastructure.Contracts
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int id);
        Task<Product> CreateProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<int> DeleteProduct(int id);

        #region Report
        Task<List<TopSellingProductReportModel>> GetBestSellingProductsLast30DaysReport();
        #endregion
    }
}
