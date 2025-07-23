using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace APIStoreManagement.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        
        
        public IClothingRepository Clothings { get; }

        public ICostsRepository Costs { get; }

        public IInventoryRepository Inventorys { get; }

        public IOrderRepository Orders { get; }

      //  public IPatternRepository Patterns { get; }

        public ISalesRepository Sales  { get; }

      //  public ISizeRepository Sizes { get; }


        public UnitOfWork(DataContext context,
                 IClothingRepository clothingRepository,
                  ICostsRepository costRepository,
                  IInventoryRepository inventoryRepository,
                  IOrderRepository orderRepository,
               //   IPatternRepository patternRepository,
               //    ISizeRepository sizeRepository,
                  ISalesRepository saleRepository
            )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
          Clothings = clothingRepository ?? throw new ArgumentNullException(nameof(clothingRepository));
            Costs = costRepository ?? throw new ArgumentNullException(nameof(costRepository));
            Inventorys = inventoryRepository ?? throw new ArgumentNullException(nameof(inventoryRepository));
            Orders = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
         //   Patterns = patternRepository ?? throw new ArgumentNullException(nameof(patternRepository));
          //  Sizes = sizeRepository ?? throw new ArgumentNullException(nameof(sizeRepository));

            Sales = saleRepository ?? throw new ArgumentNullException(nameof(saleRepository));
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}
