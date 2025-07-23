using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APIStoreManagement.Repository
{
    public class SalesRepository : ISalesRepository
    {

        private DataContext _context;
        public SalesRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<Sale> AddSaleAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        public async Task<bool> DeleteSaleAsync(int saleId)
        {
            var sale = await _context.Sales.FindAsync(saleId);
            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {saleId} not found.");
            }
            _context.Sales.Remove(sale);
             await _context.SaveChangesAsync (); 

            return true;
        }
        public async Task<ICollection<Sale>> GetAllSalesAsync(int? PatternId, int? SizeId, DateTime? startDate, DateTime? endDate)
        {
          var query=    _context.Sales.Include(s => s.Clothing)
            .ThenInclude(c => c.Size)
            .Include(s => s.Clothing)
            .ThenInclude(c => c.Pattern).AsQueryable();
            if (PatternId.HasValue && PatternId.Value > 0)
            {
                query = query.Where(s => s.Clothing.PatternId == PatternId.Value);
            }

            if (SizeId.HasValue && SizeId.Value > 0)
            {
                query = query.Where(s => s.Clothing.SizeId == SizeId.Value);
            }

            if (startDate.HasValue) // Apply start date filter
            {
                query = query.Where(s => s.Date >= startDate.Value);
            }

            if (endDate.HasValue)   // Apply end date filter
            {
   
                query = query.Where(s => s.Date <= endDate.Value.AddDays(1));
            }

            return await query.OrderBy(s => s.Id).ToListAsync();


        }

        public async Task<Sale> GetSaleAsync(int SaleId)
        {
            return await _context.Sales.FindAsync(SaleId);
        }

     

        public async Task<bool> UpdateSaleAsync(Sale sale)
        {
            var existingSale=await _context.Sales.FindAsync(sale.Id);
            if (existingSale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {sale.Id} not found.");
            }
            //Updating items
            existingSale.CustomerName = sale.CustomerName;
            existingSale.Soldprice = sale.Soldprice;
            existingSale.Date=sale.Date;
            existingSale.Description = sale.Description;
            existingSale.ClothingId = sale.ClothingId;

              await _context.SaveChangesAsync();
            return true;
        }
    }
}
