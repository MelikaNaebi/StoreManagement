using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface IPatternRepository
    {
        ICollection<Pattern> GetPatterns();
        Pattern GetPattern(int id);

        bool PatternExist(int id);
        bool CreatePattern(Pattern pattern);
        bool UpdatePattern(Pattern pattern);
        bool DeletePattern(Pattern pattern);

        bool Save();
    }
}
