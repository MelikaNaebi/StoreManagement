using Microsoft.EntityFrameworkCore.Storage;

namespace APIStoreManagement.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
       IClothingRepository Clothings { get; }
        ICostsRepository Costs { get; }
        IInventoryRepository Inventorys { get; }
        IOrderRepository Orders { get; }
      //  IPatternRepository Patterns { get; }
        ISalesRepository Sales { get; }
        //  ISizeRepository Sizes { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();

        Task<int> CompleteAsync();

    }
}
