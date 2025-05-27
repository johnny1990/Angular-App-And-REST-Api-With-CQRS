using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("top-1000-spending-users")]
        public async Task<IActionResult> GetTopSpendingUsers()
        {
            var users = await _mediator.Send(new GetTopSpendingUsersQuery());

            var result = users.Select(u => new
            {
                u.Id,
                u.Name,
                u.TotalSpent
            });

            return Ok(result);
        }
    }
}
