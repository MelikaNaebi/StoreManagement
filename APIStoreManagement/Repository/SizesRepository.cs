using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;

namespace APIStoreManagement.Repository
{
    public class SizesRepository : ISizeRepository
    {

        private DataContext _context;
        public SizesRepository(DataContext context)
        {
            _context = context;

        }
        public bool CreateSizes(Sizes sizes)
        {
            _context.Add(sizes);
            return Save();
        }

        public bool DeleteSizes(Sizes sizes)
        {
         _context.Remove(sizes);
            return Save();
        }

        public Sizes GetSize(int id)
        {
            return _context.Sizes.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Sizes> GetSizes()
        {
            return _context.Sizes.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool SizeExist(int id)
        {

            return _context.Sizes.Any(c => c.Id == id);
        }

        public bool UpdateSizes(Sizes sizes)
        {
           _context.Update(sizes);
            return Save();
        }
    }
}
