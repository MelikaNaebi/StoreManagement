using APIStoreManagement.Models;

namespace APIStoreManagement.Interfaces
{
    public interface ICostsRepository
    {
        ICollection<Costs> GetCosts();
        Costs GetCost(int id);

        bool CreateCosts(Costs costs);
        bool UpdateCosts(Costs costs);
        bool DeleteCosts(Costs costs);
        bool CostExist(int id);
        bool Save();


    }
}
