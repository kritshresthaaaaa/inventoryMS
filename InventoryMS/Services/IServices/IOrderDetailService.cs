using InventoryMS.Models.DTO;

namespace InventoryMS.Services.IServices
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetailResponseDTO>> GetOrderDetailsAsync();
    }
}
