using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface IInventoryRepository
    {
        Task<ICollection<Inventory>> GetInventoriesAsync(int? patternId = null);
        Task<Inventory> GetInventoryAsync(int InventoryId);
        Task<Inventory> GetInventoryByClothingIdAsync(int clothingId);
        Task<bool> CreateInventoryAsync(Inventory inventory); 
        Task<bool> UpdateInventoryAsync(Inventory inventory); 
        Task<bool> DeleteInventoryAsync(Inventory inventory);
    }
}
