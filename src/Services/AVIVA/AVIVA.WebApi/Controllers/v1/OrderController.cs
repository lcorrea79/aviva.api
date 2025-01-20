using AVIVa.Application.Features.Orders.Queries.GetById;
using AVIVA.Application.DTOs.Orders;
using AVIVA.Application.Features.Orders.Commands.Cancel;
using AVIVA.Application.Features.Orders.Commands.Create;
using AVIVA.Application.Features.Orders.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AVIVA.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OrderController : BaseApiController
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllOrderQuery()));
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await Mediator.Send(new GetOrderByIdQuery(id)));
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderRequestDto orderRequestDto)
        {
            _logger.LogInformation($"Creating Order");
            return Ok(await Mediator.Send(new CreateOrderCommand(orderRequestDto)));
        }

        // PUT api/<OrderController>/5
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> Cancel(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Order ID cannot be null or empty.");
            }
            _logger.LogInformation($"Canceled order with Id:{id}.");
            await Mediator.Send(new CancelOrderCommand(id));
            return Ok();
        }

        // PUT api/<OrderController>/5
        [HttpPut("pay/{id}")]
        public async Task<IActionResult> Pay(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            _logger.LogInformation($"Paid order with Id:{id}.");
            await Mediator.Send(new PayOrderCommand(id));
            return Ok();
        }

    }
}