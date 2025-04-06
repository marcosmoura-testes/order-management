using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealerController : Controller
    {
        private readonly ICreateDealer _createDealer;
        private readonly IReadDealer _readDealer;
        private readonly IUpdateDealer _updateDealer;
        private readonly IDeleteDealer _deleteDealer;

        public DealerController(ICreateDealer createDealer, IReadDealer readDealer, IUpdateDealer updateDealer, IDeleteDealer deleteDealer)
        {
            _createDealer = createDealer;
            _readDealer = readDealer;
            _updateDealer = updateDealer;
            _deleteDealer = deleteDealer;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page, int limit)
        {
            return Ok(await _readDealer.Execute(page, limit));
        }

        [HttpGet("/{{id}}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _readDealer.ExecuteById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Dealer dealer)
        {
            return Ok(await _createDealer.Execute(dealer));
        }

        [HttpPut("/{{id}}")]
        public async Task<IActionResult> Put(int id, [FromBody] Dealer dealer)
        {
            return Ok(await _updateDealer.Execute(dealer, id));
        }

        [HttpDelete("/{{id}}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _deleteDealer.Execute(id));
        }
    }
}
