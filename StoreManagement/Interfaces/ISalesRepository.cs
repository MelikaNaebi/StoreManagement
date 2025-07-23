using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface ISalesRepository
    {

       Task<Sale> AddSaleAsync (Sale sale);
       Task<ICollection<Sale>> GetAllSalesAsync(int? PatternId,int? SizeId,DateTime? startDate, DateTime? endDate);
        Task<Sale> GetSaleAsync (int SaleId);
       Task<bool> DeleteSaleAsync(int saleId);
       Task<bool> UpdateSaleAsync(Sale sale);

    }
}
