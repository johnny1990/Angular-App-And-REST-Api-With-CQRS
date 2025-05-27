using MediatR;

namespace Application.Commands
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

}
