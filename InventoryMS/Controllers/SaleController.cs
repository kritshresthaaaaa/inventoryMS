using InventoryMS.Models;
using InventoryMS.Models.DTO;
using InventoryMS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISaleService saleService, ILogger<SalesController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleResponseDTO>>> GetSales()
        {
            var sales = await _saleService.GetSalesAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDTO>> GetSale(int id)
        {
            var sale = await _saleService.GetSaleByIdAsync(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);
        }
        [HttpPost]
        public async Task<ActionResult<SaleDTO>> CreateSale(SaleDTO saleDto)
        {
            var newSale = await _saleService.CreateSaleAsync(saleDto);
            return CreatedAtAction(nameof(GetSale), new { id = newSale.Id }, newSale);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale(int id, SaleDTO saleDto)
        {
            if (id != saleDto.ProductId) 
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
        [HttpGet("per-date")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesPerDateAsync([FromQuery] DateTime date)
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
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesPerMonth([FromQuery] int year, [FromQuery] int month)
        {
            return Ok(await _saleService.GetSalesPerMonthAsync(year, month));
        }


        [HttpGet("per-week")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSalesPerWeek([FromQuery] DateTime startDate)
        {
            var utcStartDate = startDate.ToUniversalTime();
            return Ok(await _saleService.GetSalesPerWeekAsync(utcStartDate));
        }

    }
}
