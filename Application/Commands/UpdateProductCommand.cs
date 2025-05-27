using MediatR;

namespace Application.Commands
{
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public decimal Price { get; set; }
    }

}
