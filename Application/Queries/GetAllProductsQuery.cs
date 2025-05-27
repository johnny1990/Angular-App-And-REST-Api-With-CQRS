using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
    }

}
