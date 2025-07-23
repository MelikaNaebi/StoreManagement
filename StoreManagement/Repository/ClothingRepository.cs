using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace APIStoreManagement.Repository
{
    public class ClothingRepository : IClothingRepository
    {


        private DataContext _context;
        public ClothingRepository(DataContext context)
        {
            _context = context;
        }
        public bool ClothingExist(int id)
        {
            return _context.Clothings.Any(c => c.Id == id);
        }

        public bool CreateClothing(Clothing clothing)
        {
          _context.Add(clothing);
            return Save();
        }

        public bool DeleteClothing(Clothing clothing)
        {
            _context.Remove(clothing);
            return Save();
        }

        public Clothing GetClothing(int id)
        {
            return _context.Clothings.Where(c =>c.Id==id).FirstOrDefault();
        }

      
        public ICollection<Clothing> GetClothings()
        {
            return _context.Clothings.ToList();
        }

        public ICollection<Clothing> GetClothingsByPatternAndSize(int patternId, int sizeId)
        {
            return _context.Clothings.Where(c =>c.PatternId== patternId && c.SizeId==sizeId).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateClothing(Clothing clothing)
        {
           _context.Update(clothing);
            return Save();
        }

        public async Task<Clothing> GetClothingWithRelationsAsync(int clothingId)
        {
            return await _context.Clothings.Include(c => c.Pattern).Include(c => c.Size)


                .FirstOrDefaultAsync(c => c.Id == clothingId);
        }

    }
}
