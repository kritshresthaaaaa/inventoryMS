using InventoryMS.Models;
using InventoryMS.Models.DTO;

namespace InventoryMS.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task UpdateProductAsync(int id, ProductDto productDto);
        Task DeleteProductAsync(int id);
        Task SoftDeleteProductAsync(int id);
    }
}
