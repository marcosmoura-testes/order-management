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

        public OrderClientController(ISendOrderUseCase sendOrderUseCase)
        {
            _sendOrderUseCase = sendOrderUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientOrderDTO clientOrderDTO)
        {
            return Ok(await _sendOrderUseCase.Execute(clientOrderDTO));
        }
    }
}
