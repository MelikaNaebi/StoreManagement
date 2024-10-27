using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface ISalesRepository
    {
        ICollection<Sales> GetSales();
        Sales GetSale(int id);

        bool SaleExist(int id);

 
        bool CreateSales(Sales sales);
        bool UpdateSales(Sales sales);
        bool DeleteSales(Sales sales);
        bool Save();

    }
}
