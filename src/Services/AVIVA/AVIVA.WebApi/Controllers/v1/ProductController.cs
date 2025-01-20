using AVIVA.Application.DTOs.Products;
using AVIVA.Application.Features.Products.Commands.Create;
using AVIVA.Application.Features.Products.Commands.Update;
using AVIVA.Application.Features.Products.Queries.GetAll;
using AVIVA.Application.Features.Products.Queries.GetByName;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AVIVA.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductController : BaseApiController
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllProductQuery()));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> Get(string name)
        {
            return Ok(await Mediator.Send(new GetProductByNameQuery(name)));
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDto productDto)
        {
            _logger.LogInformation($"Creating Product {productDto.Name}.");
            await Mediator.Send(new CreateProductCommand(productDto));
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProductDto productDto)
        {
            if (id != productDto.Id)
            {
                BadRequest();
            }
            _logger.LogInformation($"Updating product with Id:{productDto.Id}.");
            await Mediator.Send(new UpdateProductCommand(productDto));
            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // TODO: Implement Delete
        }
    }
}