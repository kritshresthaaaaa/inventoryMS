using InventoryMS.Models.DTO;
using InventoryMS.Models.Models;

namespace InventoryMS.Services.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponseDTO>> GetCategoriesAsync();
        Task<CategoryResponseDTO> GetCategoryByIdAsync(int id);
        Task<CategoryResponseDTO> CreateCategoryAsync(CategoryPostDTO categoryPostDto);
        Task UpdateCategoryAsync(int id, Category categoryDto);
        Task DeleteCategoryAsync(int id);
    }
}
