using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);

        Task<bool> DeleteOrderAsync(int orderId);
        Task<bool> UpdateOrderAsync(Order order);

        Task<Order> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetOrdersAsync(int? patternId, int? sizeId, DateTime? startDate, DateTime? endDate);


    }
}
