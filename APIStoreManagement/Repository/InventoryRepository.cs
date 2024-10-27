using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;

namespace APIStoreManagement.Repository
{
    public class InventoryRepository : IInventoryRepository
    {

        private DataContext _context;

        public InventoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateInventory(Inventory inventory)
        {
            _context.Add(inventory);
            return Save();
        }

        public bool DeleteInventory(Inventory inventory)
        {
          _context.Remove(inventory);
            return Save();
        }

        public ICollection<Inventory> GetInventoriesByClothingId(int clothingId)
        {
            return _context.Inventories.Where(c => c.ClothingId== clothingId).ToList();
        }

      

        public Inventory GetInventory(int id)
        {
            return _context.Inventories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Inventory> GetInventorys()
        {
            return _context.Inventories.ToList();

        }

        public bool InventoryExist(int id)
        {
            return _context.Inventories.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateInventory(Inventory inventory)
        {
        _context.Update(inventory);
            return Save();
        }
    }
}
