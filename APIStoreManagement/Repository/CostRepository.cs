using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;

namespace APIStoreManagement.Repository
{
    public class CostRepository : ICostsRepository
    {
        private DataContext _context;
        public CostRepository(DataContext context)
        {
            _context=context;
        }
        public bool CostExist(int id)
        {
            return _context.Costs.Any(c => c.Id == id);
        }

        public bool CreateCosts(Costs costs)
        {
            _context.Add(costs);
            return Save();
        }

        public bool DeleteCosts(Costs costs)
        {
           _context.Remove(costs);
            return Save();
        }

        public Costs GetCost(int id)
        {
            return _context.Costs.Where( c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Costs> GetCosts()
        {
            return _context.Costs.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCosts(Costs costs)
        {
         _context.Update(costs);
            return Save();
        }
    }
}
