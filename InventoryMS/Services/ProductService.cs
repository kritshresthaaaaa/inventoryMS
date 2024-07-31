using InventoryMS.Data;
using InventoryMS.Data.Repository.IRepository;
using InventoryMS.Models;
using InventoryMS.Models.DTO;
using InventoryMS.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _repository;

        public ProductService(IGenericRepository<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Category = productDto.Category,
                Type = productDto.Type,
                Description = productDto.Description,
                Price = productDto.Price,
                StockQuantity = productDto.StockQuantity,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow
            };

            await _repository.AddAsync(product);

            productDto.Id = product.Id;
            return productDto;
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                Id = id,
                Name = product.Name,
                Category = product.Category,
                Type = product.Type,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            };
        }


        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _repository.GetAllAsync(); // _repository.GetAllAsync() returns IQueryable<Product>
  
            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category,
                Type = product.Type,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity
            }).ToList(); // corvet the projection to a list
        }

        public async Task UpdateProductAsync(int id, ProductDto productDto)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Product not found");
            }

            product.Name = productDto.Name;
            product.Category = productDto.Category;
            product.Type = productDto.Type;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.StockQuantity = productDto.StockQuantity;
            product.ModifiedDate = DateTime.UtcNow;

            await _repository.UpdateAsync(product);
        }
    }
}
