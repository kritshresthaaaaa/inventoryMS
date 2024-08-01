using InventoryMS.Models;
using InventoryMS.Models.DTO;

namespace InventoryMS.Services.IServices
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseDTO>> GetSalesAsync();
        Task<OrderResponseDTO> GetSaleByIdAsync(int id);
        Task<OrderResponseDTO> CreateSaleAsync(OrderPostDTO saleDto);
        Task UpdateSaleAsync(int id, OrderPostDTO saleDto);
        Task DeleteSaleAsync(int id);
/*        Task<IEnumerable<Order>> GetSalesPerDateAsync(DateTime date);
        Task<IEnumerable<Order>> GetSalesPerMonthAsync(int year, int month);
        Task<IEnumerable<Order>> GetSalesPerWeekAsync(DateTime startDate);*/
    }
}
