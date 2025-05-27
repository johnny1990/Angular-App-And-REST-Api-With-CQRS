using Application.Commands;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IProductRepository _repository;

        public DeleteProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id <= 0)
                    throw new ArgumentException("Invalid product ID");

                return await _repository.DeleteProduct(request.Id);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new ApplicationException("An error occurred while deleting the product.", ex);
            }
        }
    }

}
