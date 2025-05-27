using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) => Ok(id);

        [HttpPost("add-order")]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var orderId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id = orderId }, new { orderId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
