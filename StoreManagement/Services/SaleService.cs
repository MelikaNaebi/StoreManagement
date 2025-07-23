using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using AutoMapper;

namespace APIStoreManagement.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SalesDto> CreateSaleAsync(SalesDto saleDto)
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync()) {

                try
                {
                    //Inventoryconfig
                    var inventory = await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(saleDto.ClothingId);

                    if (inventory == null)
                    {

                        throw new KeyNotFoundException($"Inventory for Clothing ID {saleDto.ClothingId} not found.");
                    }
                    if (inventory.Quantity <= 0)
                    {

                        throw new InvalidOperationException("Inventory is empty. Cannot create sale.");
                    }
                    inventory.Quantity -= 1;

                    await _unitOfWork.Inventorys.UpdateInventoryAsync(inventory);

                    //Adding new record

                    var sale = new Sale
                    {
                        CustomerName = saleDto.CustomerName,
                        Soldprice = saleDto.SoldPrice,
                        Date = saleDto.Date,
                        Description = saleDto.Description,
                        Discount = saleDto.Discount,
                        ClothingId = saleDto.ClothingId,
                    };
                    await _unitOfWork.Sales.AddSaleAsync(sale);
                    await _unitOfWork.CompleteAsync();
                    await transaction.CommitAsync();

                    //clothing for sizename and patternname
                    var clothing = await _unitOfWork.Clothings.GetClothingWithRelationsAsync(sale.ClothingId);
                    if (clothing == null)
                    {
                        throw new KeyNotFoundException("لباس مورد نظر (ClothingId) یافت نشد.");
                    }

                    var resultDto = _mapper.Map<SalesDto>(sale);
                    resultDto.PatternName = clothing?.Pattern?.Name;
                    resultDto.SizeName = clothing?.Size?.SizeName;

                    return resultDto;
                }

                catch (Exception)
                {
                   
                    await transaction.RollbackAsync();
                    throw; // Exception را دوباره پرتاب می‌کنیم تا توسط کنترلر گرفته شود
                }

            }
              
        }
        

        public async Task<bool> DeleteSaleAsync(int saleId)
        {
            var sale= await _unitOfWork.Sales.GetSaleAsync(saleId);
            if (sale == null)
            {


                throw new KeyNotFoundException($"Sale with ID {saleId} not found.");
            }
            else { 

                var Inventory= await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(sale.ClothingId);
                Inventory.Quantity += 1;
                _unitOfWork.Inventorys.UpdateInventoryAsync(Inventory);
                await _unitOfWork.CompleteAsync();

           await _unitOfWork.Sales.DeleteSaleAsync(saleId);
           await _unitOfWork.CompleteAsync();
            }
            return true;
        }

        public async Task<SalesDto> GetSaleByIdAsync(int saleId)
        {
            var sale = await _unitOfWork.Sales.GetSaleAsync(saleId);
            if (sale == null) return null;
            return _mapper.Map<SalesDto>(sale);
        }

        public async Task<List<SalesDto>> GetSalesAsync(int? PatternId, int? SizeId, DateTime? startDate, DateTime? endDate)
        {
          List<Sale> saleslist= ( await _unitOfWork.Sales.GetAllSalesAsync(PatternId, SizeId, startDate, endDate)).ToList();

            var saleDto = _mapper.Map<List<SalesDto>>(saleslist);

            return saleDto;
        }

        public async Task<SalesDto> UpdateSaleAsync(int saleId, SalesDto updatedDto)
        {
            var sale = await _unitOfWork.Sales.GetSaleAsync(saleId);
            if (sale == null) {
                throw new KeyNotFoundException($"Sale with ID {saleId} not found.");
            
            }
            var oldClothingId = sale.ClothingId;
            var newClothingId = updatedDto.ClothingId;
            
            //updating items
                sale.CustomerName = updatedDto.CustomerName;
                sale.Description = updatedDto.Description;
                sale.Date = updatedDto.Date;
                sale.Soldprice = updatedDto.SoldPrice;
                sale.Discount = updatedDto.Discount;
                sale.ClothingId = newClothingId;
            //checking inventory
            if (oldClothingId != newClothingId)
            {
                var oldinventoy =await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(oldClothingId);
                oldinventoy.Quantity += 1;
                await _unitOfWork.Inventorys.UpdateInventoryAsync(oldinventoy);

                var newInventory = await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(newClothingId);
                if (newInventory.Quantity <= 0)
                    throw new InvalidOperationException("امکان ثبت فروش وجود ندارد موجودی این لباس صفر می باشد");

                newInventory.Quantity -= 1;
                await _unitOfWork.Inventorys.UpdateInventoryAsync(newInventory);
            }

             //updating
            await _unitOfWork.Sales.UpdateSaleAsync(sale);
            await _unitOfWork.CompleteAsync();

            var clothing = await _unitOfWork.Clothings.GetClothingWithRelationsAsync(sale.ClothingId);

            var resultDto = _mapper.Map<SalesDto>(sale);
            resultDto.PatternName = clothing?.Pattern?.Name;
            resultDto.SizeName = clothing?.Size?.SizeName;

            return resultDto;

        }
    }
}
