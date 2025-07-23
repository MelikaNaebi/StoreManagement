using APIStoreManagement.Dto;
using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface ISaleService
    {
        Task<SalesDto> CreateSaleAsync(SalesDto saleDto);
        Task<SalesDto> UpdateSaleAsync(int saleId,SalesDto updatedDto);
        Task<bool> DeleteSaleAsync(int saleId);
        Task<SalesDto>GetSaleByIdAsync(int saleId);
        Task<List<SalesDto>> GetSalesAsync(int? PatternId, int? SizeId, DateTime? startDate, DateTime? endDate);
    }
}
