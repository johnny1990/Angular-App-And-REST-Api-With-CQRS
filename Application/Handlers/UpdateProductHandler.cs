using Application.Commands;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, int>
    {
        private readonly IProductRepository _repository;

        public UpdateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "The request cannot be null.");

                if (request.Id <= 0)
                    throw new ArgumentException("Invalid product ID.", nameof(request.Id));

                var product = await _repository.GetProductById(request.Id);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

                product.Name = request.Name;
                product.Price = request.Price;
                var updatedProduct = await _repository.UpdateProduct(product);
                return updatedProduct.Id;
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
                throw new ApplicationException("An error occurred while updating the product.", ex);
            }
        }
    }
}
