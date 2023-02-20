using Customer.Service.Context;
using Customer.Service.Models;
using Customer.Service.ViewModels;
using Microsoft.EntityFrameworkCore;
using Order.Service.Exceptions;

namespace Customer.Service.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _context;
        public CustomerService(CustomerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerViewModel>> GetAllAsync()
        {
            return await _context.Customers
                .Where(c => c.IsDeleted == false)
                .Select(customer => new CustomerViewModel
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    Email = customer.Email,
                    PhoneNumber = customer.PhoneNumber,
                    Address = customer.Address,
                }).ToListAsync();
        }

        public async Task<CustomerViewModel> GetByIdAsync(int id)
        {
            var customerEntity = await FromId(id);
            return ToViewModel(customerEntity);
        }

        public async Task<CustomerViewModel> CreateAsync(CustomerCreateViewModel customer)
        {
            var customerEntity = ToEntity(customer);
            await _context.Customers.AddAsync(customerEntity);
            await _context.SaveChangesAsync();
            return ToViewModel(customerEntity);
        }

        public async Task<CustomerViewModel> UpdateAsync(int id, CustomerUpdateViewModel customer)
        {
            var customerEntity = await FromId(id);
            customerEntity.Name = customer.Name;
            customerEntity.Email = customer.Email;
            customerEntity.PhoneNumber = customer.PhoneNumber;
            customerEntity.Address = customer.Address;
            await _context.SaveChangesAsync();
            return ToViewModel(customerEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var customerEntity = await FromId(id);
            customerEntity.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        private CustomerViewModel ToViewModel(CustomerData customer)
        {
            return new CustomerViewModel
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
            };
        }

        private CustomerData ToEntity(CustomerCreateViewModel customer)
        {
            return new CustomerData
            {
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
            };
        }

        private async Task<CustomerData> FromId(int id)
        {
            var customerDb = await _context.Customers
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
            if(customerDb == null)
            {
                throw new RecordNotFoundException($"Could not find the Customer with id: {id}");
            }
            return customerDb;
        }
    }
}
