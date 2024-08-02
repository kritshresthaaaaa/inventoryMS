using InventoryMS.Data.Repository.IRepository;
using InventoryMS.Models.DTO;
using InventoryMS.Models.Models;
using InventoryMS.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS.Services
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository;

        public OrderDetailService(IGenericRepository<OrderDetail> orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }



      /*  public async Task<OrderDetailResponseDTO> GetOrderDetailByIdAsync(int id)
        {
            var orderDetail = await _orderDetailRepository.GetByIdAsync(id);

            return new OrderDetailResponseDTO
            {

                ProductId = orderDetail.ProductId,
                Quantity = orderDetail.Quantity,
                UnitPrice = orderDetail.Product.Price,
                ProductName = orderDetail.Product.Name,
                OrderId = orderDetail.OrderId,
                TotalPrice = orderDetail.Quantity * orderDetail.Product.Price,
            };
        }*/

        public async Task<IEnumerable<OrderDetailResponseDTO>> GetOrderDetailsAsync()
        {
            var orderDetails = await _orderDetailRepository.GetAllAsync();

            var orderDetailResponse = orderDetails.Select(
                o => new OrderDetailResponseDTO
                {
                    ProductId = o.ProductId,
                    Quantity = o.Quantity,
                    UnitPrice = o.Product.Price,
                    ProductName = o.Product.Name,
                    OrderId = o.OrderId,
                    TotalPrice = o.Quantity * o.Product.Price,
                }

                );

            return await orderDetailResponse.ToListAsync();

        }
    }
}
