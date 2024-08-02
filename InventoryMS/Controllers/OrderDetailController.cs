using InventoryMS.Models.DTO;
using InventoryMS.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailResponseDTO>>> GetOrderDetails()
        {
            var orderDetails = await _orderDetailService.GetOrderDetailsAsync();
            return Ok(orderDetails);
        }
      /*  [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<OrderDetailResponseDTO>>> GetOrderDetail(int id)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }*/

    }
}
