using InventoryMS.Models;
using InventoryMS.Models.DTO;
using InventoryMS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _saleService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService saleService, ILogger<OrderController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDTO>>> GetSales()
        {
            var sales = await _saleService.GetSalesAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderPostDTO>> GetSale(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }
        [HttpPost]
        public async Task<ActionResult<OrderPostDTO>> CreateSale(OrderPostDTO saleDto)
        {
            var newSale = await _saleService.CreateSaleAsync(saleDto);
            return CreatedAtAction(nameof(GetSale), new { id = newSale.Id }, newSale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, OrderPostDTO saleDto)
        {
            if (id != saleDto.CustomerId) 
            {
                return BadRequest();
            }

            await _saleService.UpdateSaleAsync(id, saleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            await _saleService.DeleteSaleAsync(id);
            return NoContent();
        }
      /*  [HttpGet("per-date")]
        public async Task<ActionResult<IEnumerable<Order>>> GetSalesPerDateAsync([FromQuery] DateTime date)
        {
            _logger.LogInformation($"Received date: {date}");

            var utcDate = date.ToUniversalTime();
            _logger.LogInformation($"Converted to UTC date: {utcDate}");

            var sales = await _saleService.GetSalesPerDateAsync(utcDate);

            if (sales == null || !sales.Any())
            {
                return NotFound();
            }
            return Ok(sales);
        }

        [HttpGet("per-month")]
        public async Task<ActionResult<IEnumerable<Order>>> GetSalesPerMonth([FromQuery] int year, [FromQuery] int month)
        {
            return Ok(await _saleService.GetSalesPerMonthAsync(year, month));
        }


        [HttpGet("per-week")]
        public async Task<ActionResult<IEnumerable<Order>>> GetSalesPerWeek([FromQuery] DateTime startDate)
        {
            var utcStartDate = startDate.ToUniversalTime();
            return Ok(await _saleService.GetSalesPerWeekAsync(utcStartDate));
        }*/

    }
}
