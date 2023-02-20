using Customer.Service.ViewModels;

namespace Customer.Service.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerViewModel>> GetAllAsync();
        Task<CustomerViewModel> GetByIdAsync(int id);
        Task<CustomerViewModel> CreateAsync(CustomerCreateViewModel customer);
        Task<CustomerViewModel> UpdateAsync(int id, CustomerUpdateViewModel customer);
        Task DeleteAsync(int id);
    }
}
