using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
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
        public bool CreateSales(Sales sales)
        {
            _context.Add(sales);
            return Save();
        }

        public bool DeleteSales(Sales sales)
        {
           _context.Remove(sales);
            return Save();
        }

        public Sales GetSale(int id)
        {
            return _context.Sales.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Sales> GetSales()
        {
            return _context.Sales.ToList();
        }

      

        public bool SaleExist(int id)
        {
           return _context.Sales.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSales(Sales sales)
        {
            _context.Update(sales);
            return Save();
        }
    }
}
