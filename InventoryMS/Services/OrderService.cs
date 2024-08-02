using InventoryMS.Data;
using InventoryMS.Data.Repository.IRepository;
using InventoryMS.Models;
using InventoryMS.Models.DTO;
using InventoryMS.Models.Models;
using InventoryMS.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryMS.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public OrderService(IGenericRepository<Order> saleRepository, IGenericRepository<Product> productRepository)
        {
            _orderRepository = saleRepository;
            _productRepository = productRepository;
        }

        public async Task<OrderResponseDTO> CreateSaleAsync(OrderPostDTO orderDto)
        {

            var newOrder = new Order
            {
                OrderDate = DateTime.UtcNow,
                CustomerId = orderDto.CustomerId,
                OrderDetails = new List<OrderDetail>()
            };

            decimal totalPrice = 0;
            foreach (var detail in orderDto.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {detail.ProductId} not found");
                }

                if (detail.Quantity > product.StockQuantity)
                {
                    throw new Exception($"Not enough stock available for product {product.Name}");
                }

                // Update stock quantity
                product.StockQuantity -= detail.Quantity;

                // Create OrderDetail
                var orderDetail = new OrderDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                   /* UnitPrice = product.Price,
                    TotalPrice = detail.Quantity * product.Price // Calculate total price for this detail*/
                };

                newOrder.OrderDetails.Add(orderDetail);
                /*totalPrice += orderDetail.TotalPrice; // Sum up the total price*/
            }


            await _orderRepository.AddAsync(newOrder);


            return new OrderResponseDTO
            {
                Id = newOrder.Id,
                TotalPrice = totalPrice,
                SaleDate = newOrder.OrderDate,
                ProductId = newOrder.OrderDetails.First().ProductId, // Example, you might want to customize this
                ProductName = newOrder.OrderDetails.First().Product.Name // Again, this is a simplification
            };
        }

        public async Task DeleteSaleAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }


        public async Task<OrderResponseDTO> GetSaleByIdAsync(int id)
        {
            var sale = await _orderRepository.GetByIdAsync(id);
            if (sale == null)
            {
                return null;
            }

            decimal totalPrice = 0;

            // Calculate total price by fetching the quantity and product price
            foreach (var detail in sale.OrderDetails)
            {
                totalPrice += detail.Quantity * detail.Product.Price;
            }

            var firstOrderDetail = sale.OrderDetails.FirstOrDefault();

            return new OrderResponseDTO
            {
                Id = sale.Id,
                SaleDate = sale.OrderDate,
                TotalPrice = totalPrice,
                ProductId = firstOrderDetail?.ProductId ?? 0,
                ProductName = firstOrderDetail?.Product?.Name ?? string.Empty
            };
        }


        public async Task<IEnumerable<OrderResponseDTO>> GetSalesAsync()
        {
            var salesQueryable = await _orderRepository.GetAllAsync();

    

            var saleResponse = salesQueryable.Select(s => new OrderResponseDTO
            {
                Id = s.Id,
                SaleDate = s.OrderDate,
                TotalPrice = s.OrderDetails.Sum(d => d.Quantity * d.Product.Price),
                ProductId = s.OrderDetails.First().ProductId,
                ProductName = s.OrderDetails.First().Product.Name

            });

            // here the query is not executed yet just prepared to be executed later on 
            return await saleResponse.ToListAsync(); // here the query is executed
        }
        // difference between Ienumerable and IQueryable is that IEnumerable is in-memory data and IQueryable is a query that is not executed yet

        // what does in-memory mean?
        // in-memory means that the data is stored in the memory of the application, so it is not stored in the database


        public async Task UpdateSaleAsync(int id, OrderPostDTO orderDto)
        {
            var sale = await _orderRepository.GetByIdAsync(id);
            if (sale == null)
            {
                throw new Exception("Sale not found");
            }

            sale.OrderDetails.Clear();

            // Update the order date and customer ID
            sale.OrderDate = DateTime.UtcNow;
            sale.CustomerId = orderDto.CustomerId;

            foreach (var detail in orderDto.OrderDetails)
            {
                var product = await _productRepository.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product with ID {detail.ProductId} not found");
                }

                if (detail.Quantity > product.StockQuantity)
                {
                    throw new Exception($"Not enough stock available for product {product.Name}");
                }

                // Update stock quantity
                product.StockQuantity -= detail.Quantity;

                // Create OrderDetail
                var orderDetail = new OrderDetail
                {
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                   
                };

                sale.OrderDetails.Add(orderDetail);
              
            }
            await _orderRepository.UpdateAsync(sale);
        }

        /*public async Task<IEnumerable<Order>> GetSalesPerDateAsync(DateTime date)
        {
            var utcDate = date.ToUniversalTime().Date;
            var nextDate = utcDate.AddDays(1);

            var sales = await _orderRepository.FindAsync(s => s.SaleDate >= utcDate && s.SaleDate < nextDate);

            return sales;
        }

        public async Task<IEnumerable<Order>> GetSalesPerMonthAsync(int year, int month)
        {
            return await _orderRepository.FindAsync(s => s.SaleDate.Year == year && s.SaleDate.Month == month);
        }
        public async Task<IEnumerable<Order>> GetSalesPerWeekAsync(DateTime startDate)
        {
            var utcStartDate = startDate.ToUniversalTime();
            var utcEndDate = utcStartDate.AddDays(7);
            return await _orderRepository.FindAsync(s => s.SaleDate >= utcStartDate && s.SaleDate < utcEndDate);
        }*/
    }
}
