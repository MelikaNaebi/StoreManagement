using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface ISizeRepository
    {
        ICollection<Sizes> GetSizes();
        Sizes GetSize(int id);
        bool CreateSizes(Sizes sizes);
        bool UpdateSizes(Sizes sizes);
        bool DeleteSizes(Sizes sizes);
        bool SizeExist(int id);
        bool Save();
    }
}
