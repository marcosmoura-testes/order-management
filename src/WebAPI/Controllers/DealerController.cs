using Application.Interfaces;
using Domain.Entity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        /// <summary>
        /// Retrieves a paginated list of dealers.
        /// </summary>
        /// <param name="page">The page number to retrieve.</param>
        /// <param name="limit">The number of dealers per page.</param>
        /// <returns>A list of dealers.</returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieves a paginated list of dealers.")]
        public async Task<IActionResult> Get(int page, int limit)
        {
            return Ok(await _readDealer.Execute(page, limit));
        }

        /// <summary>
        /// Retrieves a dealer by its ID.
        /// </summary>
        /// <param name="id">The ID of the dealer to retrieve.</param>
        /// <returns>The dealer.</returns>
        [HttpGet("/{{id}}")]
        [SwaggerOperation(Summary = "Retrieves a dealer by its ID.")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _readDealer.ExecuteById(id));
        }

        /// <summary>
        /// Creates a new dealer.
        /// </summary>
        /// <param name="dealer">The dealer entity to be created.</param>
        /// <returns>The created dealer entity.</returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a new dealer.")]
        public async Task<IActionResult> Post([FromBody] Dealer dealer)
        {
            return Ok(await _createDealer.Execute(dealer));
        }

        /// <summary>
        /// Updates an existing dealer.
        /// </summary>
        /// <param name="id">The ID of the dealer to be updated.</param>
        /// <param name="dealer">The dealer entity with updated information.</param>
        /// <returns>The updated dealer entity.</returns>
        [HttpPut("/{{id}}")]
        [SwaggerOperation(Summary = "Updates an existing dealer.")]
        public async Task<IActionResult> Put(int id, [FromBody] Dealer dealer)
        {
            return Ok(await _updateDealer.Execute(dealer, id));
        }

        /// <summary>
        /// Deletes a dealer by its ID.
        /// </summary>
        /// <param name="id">The ID of the dealer to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        [HttpDelete("/{{id}}")]
        [SwaggerOperation(Summary = "Deletes a dealer by its ID.")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _deleteDealer.Execute(id));
        }
    }
}
