using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;

namespace APIStoreManagement.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        private DataContext _context;
        public OrdersRepository( DataContext context)
        {
            _context = context;
            
        }
        public bool CreateOrders(Orders orders)
        {
           _context.Add(orders);
            return Save();
        }

        public bool DeleteOrders(Orders orders)
        {
            _context.Remove(orders);
            return Save();
        }

        public Orders GetOrder(int orderId)
        {
            return _context.Orders.Where(c => c.Id == orderId).FirstOrDefault();
        }

        public ICollection<Orders> GetOrders()
        {
            return _context.Orders.ToList();
        }

        public ICollection<Orders> GetOrdersBySizeIdAndSchoolId(int sizeId, int schoolId)
        {
            return _context.Orders.Where(c => c.SizeId==sizeId &&c.PatternId ==schoolId).ToList();    
        }

        public bool OrderExist(int id)
        {
            return _context.Orders.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrders(Orders orders)
        {
            _context.Update(orders);
            return Save();
        }
    }
}
