using InventoryMS.Models;
using InventoryMS.Models.DTO;

namespace InventoryMS.Services.IServices
{
    public interface ISaleService
    {
        Task<IEnumerable<SaleResponseDTO>> GetSalesAsync();
        Task<SaleResponseDTO> GetSaleByIdAsync(int id);
        Task<SaleResponseDTO> CreateSaleAsync(SaleDTO saleDto);
        Task UpdateSaleAsync(int id, SaleDTO saleDto);
        Task DeleteSaleAsync(int id);
        Task<IEnumerable<Sale>> GetSalesPerDateAsync(DateTime date);
        Task<IEnumerable<Sale>> GetSalesPerMonthAsync(int year, int month);
        Task<IEnumerable<Sale>> GetSalesPerWeekAsync(DateTime startDate);
    }
}
