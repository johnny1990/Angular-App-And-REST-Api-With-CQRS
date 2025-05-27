using Application.Commands;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProductsQuery());
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("get-product/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("Invalid product ID.");
                var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
                if (result == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            try
            {
                var productId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetProductById), new { id = productId }, new { productId });
            }
            catch (Exception ex)
            {       
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-product")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand() { Id = id });
            if (result == 0)
            {
                return BadRequest();
            }
            else
            {
                return Ok(result);
            }
        }

        #region Top Selling Products Report
        [HttpGet("top-selling-products-last-30days-report")]
        public async Task<IActionResult> GetBestSellingProductsReport()
        {
            try
            {
                var report = await _mediator.Send(new GetBestSellingProductsReportQuery());
                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        
        }
        #endregion
    }

}
