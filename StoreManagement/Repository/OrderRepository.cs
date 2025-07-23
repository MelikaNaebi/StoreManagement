using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

namespace APIStoreManagement.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private DataContext _context;
        public OrderRepository( DataContext context)
        {
            _context = context;
            
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
         var order=   await _context.Orders.FindAsync(orderId);

            if (order == null) {
                return false;
            }
            _context.Orders.Remove(order);  
        await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetOrdersAsync(int? patternId, int? sizeId, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Orders
                                .Include(o => o.Clothing)
                                    .ThenInclude(c => c.Pattern) // Include Pattern from Clothing
                                .Include(o => o.Clothing) // این خط دوم برای Include کردن Clothing تکراری است و نیازی نیست. اما اگر باشد هم مشکلی ایجاد نمیکند.
                                    .ThenInclude(c => c.Size)    // Include Size from Clothing
                                .AsQueryable();

            if (patternId.HasValue && patternId.Value > 0)
            {
                // اضافه کردن بررسی null برای Clothing
                query = query.Where(o => o.Clothing != null && o.Clothing.PatternId == patternId.Value);
            }

            if (sizeId.HasValue && sizeId.Value > 0)
            {
                // اضافه کردن بررسی null برای Clothing
                query = query.Where(o => o.Clothing != null && o.Clothing.SizeId == sizeId.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(o => o.OrderDate.Date >= startDate.Value.Date);
            }

            if (endDate.HasValue)
            {
                query = query.Where(o => o.OrderDate.Date <= endDate.Value.Date);
            }

            return await query.OrderBy(o => o.Id).ToListAsync();
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                                 .Include(o => o.Clothing)
                                     .ThenInclude(c => c.Pattern) // This line is crucial
                                 .Include(o => o.Clothing)
                                     .ThenInclude(c => c.Size)    // This line is crucial
                                 .FirstOrDefaultAsync(o => o.Id == orderId);
        }



        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.Id);
            if (existingOrder == null) {

                return false;
            }
            //updating items
            existingOrder.Deposit = order.Deposit;
            existingOrder.Description = order.Description;
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.CustomerPhoneNumber = order.CustomerPhoneNumber;
            existingOrder.ClothingId = order.ClothingId;
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.IsDelivered = order.IsDelivered;

            await _context.SaveChangesAsync();
            return true;


        }
    }
}
