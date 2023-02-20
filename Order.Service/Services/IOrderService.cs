using Order.Service.ViewModels;

namespace Order.Service.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllAsync();
        Task<OrderViewModel> GetByIdAsync(int id);
        Task<OrderViewModel> CreateAsync(OrderCreateViewModel order);
        Task<OrderViewModel> UpdateAsync(int id, OrderUpdateViewModel order);
        Task DeleteAsync(int id);
    }
}
