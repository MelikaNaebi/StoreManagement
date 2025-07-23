using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using Microsoft.EntityFrameworkCore; // برای Include
namespace APIStoreManagement.Repository
{
    public class InventoryRepository : IInventoryRepository
    {

        private   readonly DataContext _context;

        public InventoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateInventoryAsync(Inventory inventory)
        {
            await _context.AddAsync(inventory);
            return true;
        }

        public async Task<bool> DeleteInventoryAsync(Inventory inventory)
        {
            _context.Remove(inventory);
            return true;
        }

        public async Task<ICollection<Inventory>> GetInventoriesAsync(int? patternId = null)
        {
            var query = _context.Inventories
                                 .Include(i => i.Clothing)
                                     .ThenInclude(c => c.Pattern)
                                 .Include(i => i.Clothing)
                                     .ThenInclude(c => c.Size)
                                 .AsQueryable(); // برای امکان اضافه کردن شرط، از AsQueryable() استفاده می‌کنیم.

            if (patternId.HasValue && patternId.Value > 0)
            {
                query = query.Where(i => i.Clothing.PatternId == patternId.Value);
            }

            return await query.OrderBy(i => i.Id).ToListAsync();
        }

        public async Task<Inventory> GetInventoryAsync(int InventoryId)
        {
            return await _context.Inventories
                                 .Include(i => i.Clothing)
                                     .ThenInclude(c => c.Pattern)
                                 .Include(i => i.Clothing)
                                     .ThenInclude(c => c.Size)
                                 .Where(c => c.Id == InventoryId)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Inventory> GetInventoryByClothingIdAsync(int clothingId)
        {
            return await _context.Inventories
                                 .Include(i => i.Clothing)
                                     .ThenInclude(c => c.Pattern)
                                 .Include(i => i.Clothing)
                                     .ThenInclude(c => c.Size)
                                 .Where(c => c.ClothingId == clothingId)
                                 .FirstOrDefaultAsync();
        }

     
      

        public async Task<bool> UpdateInventoryAsync(Inventory inventory)
        {
            _context.Inventories.Update(inventory);
            return true;
        }
    }
}
