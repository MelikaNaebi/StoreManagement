
using APIStoreManagement.Models;


namespace APIStoreManagement.Interfaces
{
    public interface IClothingRepository
    {
        ICollection<Clothing> GetClothings();
        Clothing GetClothing(int id);
        
        Task<Clothing> GetClothingWithRelationsAsync(int clothingId);

        ICollection<Clothing> GetClothingsByPatternAndSize(int schoolId, int sizeId);

        bool CreateClothing(Clothing clothing);
        bool UpdateClothing(Clothing clothing);
        bool DeleteClothing(Clothing clothing);
        bool ClothingExist(int id);

        bool Save();
    }
}
