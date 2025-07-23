using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using AutoMapper;

namespace APIStoreManagement.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
        {
            var inventory = await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(orderCreateDto.ClothingId);
            if (inventory == null)
            {

                throw new KeyNotFoundException($"Inventory for Clothing ID {orderCreateDto.ClothingId} not found.");
            }
            if (inventory.Quantity > 0)
            {

                throw new InvalidOperationException("موجودی این لباس صفر نمیباشد و امکان ثبت سفارش وجود نداره");
            }
            var order = _mapper.Map<Order>(orderCreateDto);
            order.OrderDate = DateTime.Now;
            order.IsDelivered = false;

            await _unitOfWork.Orders.AddOrderAsync(order);
            await _unitOfWork.CompleteAsync();
            var createdOrderWithRelations = await _unitOfWork.Orders.GetOrderByIdAsync(order.Id);
            if (createdOrderWithRelations == null)
            {
                throw new InvalidOperationException("Failed to retrieve created order after saving.");

            }
            return _mapper.Map<OrderDto>(createdOrderWithRelations);
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            bool deleted = await _unitOfWork.Orders.DeleteOrderAsync(orderId);
            if (!deleted)
            {
                throw new KeyNotFoundException($"Order with Id {orderId} not found.");
            }
            return true;
        }

        public async Task<OrderDto> GetOrderAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new KeyNotFoundException($" order with id {orderId} not found");
            }
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<List<OrderDto>> GetOrdersAsync(int? PatternId, int? SizeId, DateTime? StartDate, DateTime? EndDate)
        {
            List<Order> ordersList =( await _unitOfWork.Orders.GetOrdersAsync(PatternId, SizeId, StartDate, EndDate)).ToList();

            var orderDtoList = _mapper.Map<List<OrderDto>>(ordersList);
            return orderDtoList;
        }

        public async Task<SalesDto> ProcessOrderToSaleAsync(int OrderId,ProcessOrderToSaleDto processDto)
        {
            var Order= await _unitOfWork.Orders.GetOrderByIdAsync((int)OrderId);
            if (Order == null)
            {
                throw new KeyNotFoundException($" Order with {OrderId} Not found");
            }
            var inventory = await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(Order.ClothingId);
            if (inventory.Quantity <= 0  ||inventory ==null)
            {
                throw new InvalidOperationException($"Inventory for this order not found or is empty Cannot process sale.");
            }

            //adding to sale
            var newSale = new Sale{

                CustomerName=Order.CustomerName,
                ClothingId=Order.ClothingId,
                Date = DateTime.UtcNow,
                Soldprice = processDto.SoldPrice, // از ورودی کاربر
                Discount = processDto.Discount,   // از ورودی کاربر
                InitialDeposit = Order.Deposit,
                
                Description = $"فروش ناشی از سفارش تولید (ID: {Order.Id}). {processDto.SaleDescription ?? ""}" // ترکیب توضیحات

            };
            await _unitOfWork.Sales.AddSaleAsync(newSale);

            //inventory config

            if (inventory != null && inventory.Quantity>0) {
                inventory.Quantity--;
                await _unitOfWork.Inventorys.UpdateInventoryAsync(inventory);
            
            }
            //removing from order
          await   _unitOfWork.Orders.DeleteOrderAsync(OrderId);
          await  _unitOfWork.CompleteAsync();

            return _mapper.Map<SalesDto>(newSale);

        }

        public async Task<OrderDto> UpdateOrderAsync(int orderId, OrderDto updatedDto)
        {
            var exsitingOrder = await _unitOfWork.Orders.GetOrderByIdAsync(orderId);
            if (exsitingOrder == null)
            {
                throw new KeyNotFoundException($"Order with {orderId} not found ");

            }

            var inventory = await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(updatedDto.ClothingId);
            if (inventory == null)
            {

                throw new KeyNotFoundException($"Inventory for Clothing ID {updatedDto.ClothingId} not found.");
            }
            if (inventory.Quantity > 0)
            {

                throw new InvalidOperationException("موجودی این لباس صفر نمیباشد و امکان ثبت سفارش وجود نداره");
            }

            _mapper.Map(updatedDto, exsitingOrder);

            bool updateResult = await _unitOfWork.Orders.UpdateOrderAsync(exsitingOrder);
            if (!updateResult)
            {
                throw new InvalidOperationException("Failed to update order in repository.");
            }

            await _unitOfWork.CompleteAsync();

            var updatedAndLoadedOrder = await _unitOfWork.Orders.GetOrderByIdAsync(orderId);
            if (updatedAndLoadedOrder == null)
            {
                throw new InvalidOperationException("Failed to retrieve updated order after saving.");

            }
            return _mapper.Map<OrderDto>(updatedAndLoadedOrder);
        }

    }
}

