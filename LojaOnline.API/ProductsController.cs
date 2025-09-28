using LojaOnline.Application.Commands.CreateProduct;
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
    }
}
