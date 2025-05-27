using Application.Commands;
using Domain.Entities;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repository;

        public CreateProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name) || request.Price < 0)
                    throw new ArgumentException("Invalid data");

                var product = new Product
                {
                    Name = request.Name,
                    Price = request.Price
                };
                var createdProduct = await _repository.CreateProduct(product);
                return createdProduct.Id;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                throw new ApplicationException("An unexpected error occurred while creating the product.", ex);
            }
        }
    }
}
