using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetBestSellingProductsReportQuery : IRequest<List<TopSellingProductReportModel>> 
    { 
    }
}
