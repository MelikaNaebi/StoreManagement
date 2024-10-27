using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface IInventoryRepository
    {
        ICollection<Inventory> GetInventorys();
        Inventory GetInventory(int id);
        
        bool InventoryExist(int id);

        ICollection<Inventory> GetInventoriesByClothingId(int clothingId);

       


        bool CreateInventory(Inventory inventory);
        bool UpdateInventory(Inventory inventory);
        bool DeleteInventory(Inventory inventory);
        bool Save();
    }
}
