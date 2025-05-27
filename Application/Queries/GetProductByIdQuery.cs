using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetProductByIdQuery : IRequest<Product>
    {
        public int Id { get; set; }
    }

}
