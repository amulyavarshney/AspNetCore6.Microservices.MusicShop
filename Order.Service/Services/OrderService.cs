using Azure;
using Microsoft.EntityFrameworkCore;
using Order.Service.Context;
using Order.Service.Exceptions;
using Order.Service.Extensions;
using Order.Service.Models;
using Order.Service.ViewModels;

namespace Order.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrderContext _context;
        private readonly HttpClient _client;
        public OrderService(OrderContext context, HttpClient client)
        {
            _context = context;
            _client = client;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllAsync()
        {
            var orders = await _context.Orders
                // .Where(c => c.IsDeleted == false)
                .ToListAsync();
            var orderViewModels = new List<OrderViewModel>();
            foreach(var order in orders)
            {
                var customer = await GetCustomerAsync(order.CustomerId);
                var orderViewModel = new OrderViewModel
                {
                    Id = order.Id,
                    CustomerId = order.CustomerId,
                    CustomerName = customer.Name,
                    CustomerEmail = customer.Email,
                    CustomerPhone = customer.PhoneNumber,
                    Date = order.Date,
                    PaymentTerms = order.PaymentTerms,
                    ShippingAddress = order.ShippingAddress,
                    // ItemId = order.ItemId,
                };
                orderViewModels.Add(orderViewModel);
            }
            return orderViewModels;
        }

        public async Task<OrderViewModel> GetByIdAsync(int id)
        {
            var orderEntity = await FromId(id);
            return await ToViewModel(orderEntity);
        }

        public async Task<OrderViewModel> CreateAsync(OrderCreateViewModel order)
        {
            var customer = await GetCustomerAsync(order.CustomerId);
            var orderEntity = ToEntity(order);
            await _context.Orders.AddAsync(orderEntity);
            await _context.SaveChangesAsync();
            return await ToViewModel(orderEntity);
        }

        public async Task<OrderViewModel> UpdateAsync(int id, OrderUpdateViewModel order)
        {
            var orderEntity = await FromId(id);
            orderEntity.PaymentTerms = order.PaymentTerms;
            orderEntity.ShippingAddress = order.ShippingAddress;
            // orderEntity.ItemId = order.ItemId;
            await _context.SaveChangesAsync();
            return await ToViewModel(orderEntity);
        }

        public async Task DeleteAsync(int id)
        {
            var orderEntity = await FromId(id);
            // orderEntity.IsDeleted = true;
            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();
            
        }

        private async Task<OrderViewModel> ToViewModel(Models.Order order)
        {
            var customer = await GetCustomerAsync(order.CustomerId);
            return new OrderViewModel
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                CustomerName = customer.Name,
                CustomerEmail = customer.Email,
                CustomerPhone = customer.PhoneNumber,
                Date = order.Date,
                PaymentTerms = order.PaymentTerms,
                ShippingAddress = order.ShippingAddress,
                // ItemId = order.ItemId,
            };
        }

        private Models.Order ToEntity(OrderCreateViewModel order)
        {
            return new Models.Order
            {
                CustomerId = order.CustomerId,
                Date = order.Date,
                PaymentTerms = order.PaymentTerms,
                ShippingAddress = order.ShippingAddress,
                // ItemId = order.ItemId,
            };
        }

        private async Task<Models.Order> FromId(int id)
        {
            var orderDb = await _context.Orders
                // .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Id == id);
            if(orderDb is null)
            {
                throw new RecordNotFoundException($"Could not find the Order with id: {id}");
            }
            return orderDb;
        }
        private async Task<Customer> GetCustomerAsync(int customerId)
        {
            var customer = await _client.GetAsync($"/api/v1/customer/{customerId}");
            return await customer.GetAs<Customer>();
        }
    }
}
