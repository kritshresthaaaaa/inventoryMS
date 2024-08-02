using InventoryMS.Data.Repository.IRepository;
using InventoryMS.Models;
using InventoryMS.Models.DTO;
using InventoryMS.Models.Models;
using InventoryMS.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace InventoryMS.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _repository;
        public CustomerService(IGenericRepository<Customer> repository)
        {
            _repository = repository;
        }
        public async Task<CustomerResponseDTO> CreateCustomerAsync(CustomerPostDTO customerPostDto)
        {
            var newCustomer = new Customer
            {
                FirstName = customerPostDto.FirstName,
                LastName = customerPostDto.LastName,
                Email = customerPostDto.Email,
                PhoneNumber = customerPostDto.PhoneNumber,
                Address = customerPostDto.Address,

            };
            await _repository.AddAsync(newCustomer);
            return new CustomerResponseDTO
            {
                CustomerId = newCustomer.CustomerId,
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Email = newCustomer.Email,
                PhoneNumber = newCustomer.PhoneNumber,
                Address = newCustomer.Address
            };
        }

        public Task DeleteCustomerAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            return new CustomerResponseDTO
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address

            };

        }

        public async Task<IEnumerable<CustomerResponseDTO>> GetCustomersAsync()
        {
            var customers = await _repository.GetAllAsync();
            return customers.Select(c => new CustomerResponseDTO
            {
                CustomerId = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address,
            }).ToList();

        }

        public Task UpdateCustomerAsync(int id, CustomerPostDTO customerPostDto)
        {
            throw new NotImplementedException();
        }
    }
}
