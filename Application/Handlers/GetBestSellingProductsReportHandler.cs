using Application.Queries;
using Domain.Models;
using Infrastructure.Contracts;
using MediatR;

namespace Application.Handlers
{
    public class GetBestSellingProductsReportHandler : IRequestHandler<GetBestSellingProductsReportQuery, List<TopSellingProductReportModel>>
    {
        private readonly IProductRepository _repository;

        public GetBestSellingProductsReportHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TopSellingProductReportModel>> Handle(GetBestSellingProductsReportQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "Request cannot be null.");

                var report = await _repository.GetBestSellingProductsLast30DaysReport();
                return report;
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Request Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                throw new ApplicationException("An error occurred while generating the best-selling products report.", ex);
            }
        }

    }
}
