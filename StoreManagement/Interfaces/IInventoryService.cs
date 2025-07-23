using APIStoreManagement.Dto;

namespace APIStoreManagement.Interfaces
{
    public interface IInventoryService
    {
        Task<List<InventoryDto>> GetInventoriesAsync(int? patternId = null);
        Task<InventoryDto> GetInventoryAsync(int inventoryId);
        Task<InventoryDto> GetInventoryByClothingIdAsync(int clothingId);
        Task<InventoryDto> CreateInventoryAsync(InventoryCreateUpdateDto inventoryDto);
        Task<InventoryDto> UpdateInventoryAsync(int inventoryId, InventoryCreateUpdateDto inventoryDto);
        Task DeleteInventoryAsync(int inventoryId);

    }
}
