using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderClientController : Controller
    {
        private readonly ISendOrderUseCase _sendOrderUseCase;
        private readonly IRequestOrderUseCase _requestOrderUseCase;

        public OrderClientController(ISendOrderUseCase sendOrderUseCase, IRequestOrderUseCase requestOrderUseCase)
        {
            _sendOrderUseCase = sendOrderUseCase;
            _requestOrderUseCase = requestOrderUseCase;
        }

        /// <summary>
        /// Sends a client order.
        /// </summary>
        /// <param name="clientOrderDTO">The client order details.</param>
        /// <returns>The result of the send operation.</returns>
        [HttpPost("/SendOrder")]
        [SwaggerOperation(Summary = "Sends a client order", Description = "Executes the process of sending a client order.")]
        [SwaggerResponse(200, "The client order was sent successfully.")]
        [SwaggerResponse(400, "The client order details are invalid.")]
        public async Task<IActionResult> SendOrder([FromBody] ClientOrderDTO clientOrderDTO)
        {
            return Ok(await _sendOrderUseCase.Execute(clientOrderDTO));
        }
    }
}
