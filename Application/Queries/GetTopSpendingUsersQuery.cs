using Domain.Models;
using MediatR;

namespace Application.Queries
{
    public class GetTopSpendingUsersQuery : IRequest<List<TopSpendingUsersModel>> 
    { 
    }
}
