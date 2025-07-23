using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
    
        public OrdersController(IOrderService orderService )
        {
            _orderService = orderService;
       
        }

        [HttpPost("{orderId}/process-to-sale")]
        public async Task<IActionResult> ProcessOrderToSale(int OrderId, [FromBody] ProcessOrderToSaleDto processDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var salesDto = await _orderService.ProcessOrderToSaleAsync(OrderId, processDto);
                if (salesDto == null)
                {
                    return NotFound($"Order with ID {OrderId} could not be processed or not found.");
                }

                return Ok(salesDto); 
            }

            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing order to sale for OrderId {OrderId}: {ex.Message} \n StackTrace: {ex.StackTrace}");
                return StatusCode(500, new { message = "An error occurred while processing the order to sale.", details = ex.Message });
            }
        }

            
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] int? PatternId, int? SizeId, DateTime? StartDate, DateTime? EndDate)
        {

           var Orders = await _orderService.GetOrdersAsync(PatternId, SizeId, StartDate, EndDate);
            return Ok(Orders);

        }

        [HttpGet("{OrderId}")]
        [ProducesResponseType(200, Type = typeof(OrderDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<OrderDto>> GetOrderById(int OrderId)
        {
            try
            {
                var order = await _orderService.GetOrderAsync(OrderId);
                if (order == null) // اگر Service Null برگرداند (در صورت عدم یافتن)
                {
                    return NotFound($"Order with Id {OrderId} not found.");
                }
                return Ok(order);
            
            }
            catch (KeyNotFoundException ex) // اگر Service Exception پرتاب کند
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(OrderDto))]
        [ProducesResponseType(400)]

        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var createOrder = await _orderService.CreateOrderAsync(orderDto);

                return CreatedAtAction(nameof(GetOrders), new { id = createOrder.Id }, createOrder);
            }
            catch (InvalidOperationException ex) // اگر Service خطای عملیاتی پرتاب کند
            {
                return BadRequest(new { message = ex.Message });
            }


        }

        [HttpPut("{OrderId}")]
        [ProducesResponseType(200, Type = typeof(OrderDto))] 
        [ProducesResponseType(400)] 
        [ProducesResponseType(404)] 

        public async Task<IActionResult>UpdateOrder([FromBody]OrderDto orderDto,int OrderId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var reslut = await _orderService.UpdateOrderAsync(OrderId, orderDto);
                return Ok(reslut);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        [HttpDelete("{OrderId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteOrder(int OrderId)
        {
            try { 
            await _orderService.DeleteOrderAsync(OrderId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}

