using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;

namespace APIStoreManagement.Repository
{
    public class PatternRepository : IPatternRepository
    {
        private DataContext _context;
        public PatternRepository(DataContext context)
        {
            _context = context;

        }
        public bool CreatePattern(Pattern pattern)
        {
            _context.Add(pattern);
            return Save();
        }

        public bool DeletePattern(Pattern pattern)
        {
            _context.Remove(pattern);
            return Save();
        }

        public Pattern GetPattern(int id)
        {
            return _context.Patterns.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Pattern> GetPatterns()
        {
           return _context.Patterns.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool PatternExist(int id)
        {
            return _context.Patterns.Any(c => c.Id == id);
        }

        public bool UpdatePattern(Pattern pattern)
        { 
            _context.Update(pattern);
            return Save();
        }
    }
}
