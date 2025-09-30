using LojaOnline.Application.Commands.CreateProduct;
using LojaOnline.Application.Commands.DeleteProduct;
using LojaOnline.Application.Commands.UpdateProduct;
using LojaOnline.Application.Queries.GetProduct;
using LojaOnline.Application.Queries.GetProductById;
using LojaOnline.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LojaOnline.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> Get()
        {
            var result = await _mediator.Send(new GetProductQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);

            if(product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductViewModel>> Update(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "Id da URL não corresponde ao do body" });

            var updatedProduct = await _mediator.Send(command);

            return Ok(updatedProduct);
        }

    }
}
