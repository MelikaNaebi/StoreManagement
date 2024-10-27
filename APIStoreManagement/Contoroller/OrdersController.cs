using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using APIStoreManagement.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _OrdersRepository;
        private readonly ISizeRepository _SizeRepository;
        private readonly IPatternRepository _patternRepository;
        private readonly IMapper _mapper;
        public OrdersController(IOrdersRepository OrdersController, IMapper mapper, ISizeRepository sizeRepository, IPatternRepository schoolRepository)
        {
            _OrdersRepository = OrdersController;
            _mapper = mapper;
            _SizeRepository = sizeRepository;
            _patternRepository = schoolRepository;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Orders>))]
        public IActionResult GetOrders()
        {

            var orders = _mapper.Map<List<OrderDto>>(_OrdersRepository.GetOrders());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(orders);
        }
        [HttpGet("{orderid}")]
        [ProducesResponseType(200, Type = typeof(Orders))]
        [ProducesResponseType(400)]
        public IActionResult GetOrder(int id)
        {
            if (!_OrdersRepository.OrderExist(id))
                return NotFound();
            var order = _mapper.Map<OrderDto>(_OrdersRepository.GetOrder(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(order);
        }

        [HttpGet("OrdersBySchoolAndSize/{schoolId}/{sizeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetOrdersBySizeIdAndSchoolId(int schoolId, int sizeId)
        {
            var orders = _mapper.Map<List<OrderDto>>(_OrdersRepository.GetOrdersBySizeIdAndSchoolId(schoolId, sizeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(orders);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOrders([FromQuery] int sizeId, [FromQuery] int patternId, [FromBody] OrderDto orderCreate)
        {
            if (orderCreate == null)
                return BadRequest(ModelState);



            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderMap = _mapper.Map<Orders>(orderCreate);

            orderMap.Sizes = _SizeRepository.GetSize(sizeId);
            orderMap.Pattern = _patternRepository.GetPattern(patternId);

            if (!_OrdersRepository.CreateOrders(orderMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }
        [HttpPut("{orderid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateOrders(int orderid, [FromBody] OrderDto updatedorder)
        {
            if (updatedorder == null)
            {
                return BadRequest("Invalid order data.");
            }
            if (orderid != updatedorder.Id)
            {
                return BadRequest("ID mismatch.");
            }
            if (!_OrdersRepository.OrderExist(updatedorder.Id))
            {
                return NotFound();
            }
            if (!_SizeRepository.SizeExist(updatedorder.SizeId))
            {
                return BadRequest("Invalid SizeId.");
            }
            if (!_patternRepository.PatternExist(updatedorder.PatternId))
            {
                return BadRequest("Invalid SchoolId.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ordermap = _mapper.Map<Orders>(updatedorder);
            if (!_OrdersRepository.UpdateOrders(ordermap))
            {
                ModelState.AddModelError("", "Something went wrong updating order");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{orderid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteOrders(int orderid)
        {

            var orderToDelete = _OrdersRepository.GetOrder(orderid);
            if (orderToDelete == null)
            {
                ModelState.AddModelError("", "order not found");
                return NotFound(ModelState);
            }


            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_OrdersRepository.DeleteOrders(orderToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting order");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

