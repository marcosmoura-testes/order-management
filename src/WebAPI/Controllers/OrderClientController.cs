using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost("/SendOrder")]
        public async Task<IActionResult> SendOrder([FromBody] ClientOrderDTO clientOrderDTO)
        {
            return Ok(await _sendOrderUseCase.Execute(clientOrderDTO));
        }

        [HttpPost("/ResquestOrder")]
        public async Task<IActionResult> ResquestOrder([FromBody] RequestOrderDTO requestOrderDTO)
        {
            return Ok(await _requestOrderUseCase.Execute(requestOrderDTO));
        }
    }
}
