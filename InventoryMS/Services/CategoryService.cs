using InventoryMS.Data.Repository.IRepository;
using InventoryMS.Models.DTO;
using InventoryMS.Models.Models;
using InventoryMS.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repository;
        public CategoryService(IGenericRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task<CategoryResponseDTO> CreateCategoryAsync(CategoryPostDTO categoryPostDto)
        {
            var category = new Category
            {
                CategoryName = categoryPostDto.CategoryName
            };
            await _repository.AddAsync(category);

            return new CategoryResponseDTO
            {
                Id = category.CategoryId,
                Name = category.CategoryName
            };

        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryResponseDTO>> GetCategoriesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(c => new CategoryResponseDTO
            {
                Id = c.CategoryId,
                Name = c.CategoryName
            }).ToList();

        }

        public async Task<CategoryResponseDTO> GetCategoryByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return new CategoryResponseDTO
            {
                Id = category.CategoryId,
                Name = category.CategoryName
            };
    
 
        }

        public Task UpdateCategoryAsync(int id, Category categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
