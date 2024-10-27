using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface IOrdersRepository
    {
        ICollection<Orders> GetOrders();
        Orders GetOrder(int orderId);

      
        ICollection<Orders>GetOrdersBySizeIdAndSchoolId(int sizeId, int schoolId);

        bool CreateOrders(Orders orders);
        bool UpdateOrders(Orders orders);
        bool DeleteOrders(Orders orders);

        bool OrderExist(int id);
        bool Save();
    }
}
