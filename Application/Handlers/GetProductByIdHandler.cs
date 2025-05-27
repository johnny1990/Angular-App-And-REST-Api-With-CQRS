using Application.Queries;
using Domain.Entities;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        private readonly IProductRepository _repository;

        public GetProductByIdHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "The request cannot be null.");

                if (request.Id <= 0)
                    throw new ArgumentException("Invalid product ID.", nameof(request.Id));

                var product = await _repository.GetProductById(request.Id);

                if (product == null)
                    throw new KeyNotFoundException($"No product with ID {request.Id}  found.");

                return product;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
                throw;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"Not Found Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new ApplicationException("An error occurred while retrieving the product.", ex);
            }
        }
    }
}
