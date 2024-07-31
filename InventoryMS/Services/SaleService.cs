using InventoryMS.Data;
using InventoryMS.Data.Repository.IRepository;
using InventoryMS.Models;
using InventoryMS.Models.DTO;
using InventoryMS.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryMS.Services
{
    public class SaleService : ISaleService
    {
        private readonly IGenericRepository<Sale> _saleRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public SaleService(IGenericRepository<Sale> saleRepository, IGenericRepository<Product> productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task<SaleResponseDTO> CreateSaleAsync(SaleDTO saleDto)
        {
            var product = await _productRepository.GetByIdAsync(saleDto.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (saleDto.Quantity > product.StockQuantity)
            {
                throw new Exception("Not enough stock available");
            }

           var newSale = new Sale
            {
                Quantity = saleDto.Quantity,
                TotalPrice = saleDto.Quantity * product.Price,
                SaleDate = DateTime.UtcNow,
                ProductId = saleDto.ProductId
            };

            product.StockQuantity -= saleDto.Quantity;

            await _saleRepository.AddAsync(newSale);
            await _productRepository.UpdateAsync(product);

            return new SaleResponseDTO
            {
                Id = newSale.Id,
                Quantity = newSale.Quantity,
                TotalPrice = newSale.TotalPrice,
                SaleDate = newSale.SaleDate,
                ProductId = newSale.ProductId,
                ProductName = product.Name
            };
        }

        public async Task DeleteSaleAsync(int id)
        {
            await _saleRepository.DeleteAsync(id);
        }


        public async Task<SaleResponseDTO> GetSaleByIdAsync(int id)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                return null;
            }

            return new SaleResponseDTO
            {
                Id = sale.Id,
                Quantity = sale.Quantity,
                TotalPrice = sale.TotalPrice,
                SaleDate = sale.SaleDate,
                ProductId = sale.ProductId,
                ProductName = sale.Product.Name
            };
            
        }

        public async Task<IEnumerable<SaleResponseDTO>> GetSalesAsync()
        {
            var salesQueryable = await _saleRepository.GetAllAsync();

            var saleResponse = salesQueryable.Select(s => new SaleResponseDTO
            {
                Id = s.Id,
                Quantity = s.Quantity,
                TotalPrice = s.TotalPrice,
                SaleDate = s.SaleDate,
                ProductId = s.ProductId,
                ProductName = s.Product.Name
            });

            // here the query is not executed yet just prepared to be executed later on 
            return await saleResponse.ToListAsync(); // here the query is executed
        }
        // difference between Ienumerable and IQueryable is that IEnumerable is in-memory data and IQueryable is a query that is not executed yet

        // what does in-memory mean?
        // in-memory means that the data is stored in the memory of the application, so it is not stored in the database


        public async Task UpdateSaleAsync(int id, SaleDTO saleDto)
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
            {
                throw new Exception("Sale not found");
            }

            sale.Quantity = saleDto.Quantity;
            sale.ProductId = saleDto.ProductId;
            sale.TotalPrice = saleDto.Quantity * (await _productRepository.GetByIdAsync(saleDto.ProductId)).Price;

            await _saleRepository.UpdateAsync(sale);
        }

        public async Task<IEnumerable<Sale>> GetSalesPerDateAsync(DateTime date)
        {
            var utcDate = date.ToUniversalTime().Date;
            var nextDate = utcDate.AddDays(1);

            var sales = await _saleRepository.FindAsync(s => s.SaleDate >= utcDate && s.SaleDate < nextDate);

            return sales;
        }

        public async Task<IEnumerable<Sale>> GetSalesPerMonthAsync(int year, int month)
        {
            return await _saleRepository.FindAsync(s => s.SaleDate.Year == year && s.SaleDate.Month == month);
        }
        public async Task<IEnumerable<Sale>> GetSalesPerWeekAsync(DateTime startDate)
        {
            var utcStartDate = startDate.ToUniversalTime();
            var utcEndDate = utcStartDate.AddDays(7);
            return await _saleRepository.FindAsync(s => s.SaleDate >= utcStartDate && s.SaleDate < utcEndDate);
        }
    }
}
