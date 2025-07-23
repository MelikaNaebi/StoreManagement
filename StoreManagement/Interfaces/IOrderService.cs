using APIStoreManagement.Dto;

namespace APIStoreManagement.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto);
        Task<bool> DeleteOrderAsync(int orderId);

        Task<OrderDto> GetOrderAsync(int orderId);
        Task<OrderDto> UpdateOrderAsync(int orderId , OrderDto updatedDto);

        Task<List<OrderDto>> GetOrdersAsync(int? PatternId, int? SizeId, DateTime? StartDate, DateTime? EndDate);

        Task<SalesDto> ProcessOrderToSaleAsync(int OrderId, ProcessOrderToSaleDto processDto);

    }
}
