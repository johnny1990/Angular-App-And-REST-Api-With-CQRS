using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetProductByIdQuery : IRequest<Product>, IBaseRequest
    {
        public int Id { get; set; }

        // Adding a constructor to fix CS1729 error  
        public GetProductByIdQuery(int id)
        {
            Id = id;
        }
    }

}
