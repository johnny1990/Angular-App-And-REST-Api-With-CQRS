using Application.Queries;
using Domain.Entities;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "The request cannot be null.");

                var products = await _repository.GetAllProducts();
                return products.ToList();
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new ApplicationException("An error occurred while retrieving the product list.", ex);
            }
        }

    }

}
