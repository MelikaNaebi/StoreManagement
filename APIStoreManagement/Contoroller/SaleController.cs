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
    public class SaleController : Controller
    {
        private readonly ISalesRepository _salesRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IClothingRepository _clothingRepository;
        private readonly IMapper _mapper;
        public SaleController(ISalesRepository salesRepository , IMapper mapper, IInventoryRepository inventoryRepository,IClothingRepository clothingRepository )
        {
            _salesRepository = salesRepository;
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
            _clothingRepository = clothingRepository;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sales>))]
        public IActionResult GetSales()
        {

            var sales = _mapper.Map<List<SalesDto>>(_salesRepository.GetSales());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(sales);
        }

        [HttpGet("{saleid}")]
        [ProducesResponseType(200, Type = typeof(Sales))]
        [ProducesResponseType(400)]
        public IActionResult GetSale(int id)
        {
            if (!_salesRepository.SaleExist(id))
                return NotFound();
            var sale = _mapper.Map<SalesDto>(_salesRepository.GetSale(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(sale);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateSales( [FromQuery] int inventoryId, [FromQuery] int clothingId, [FromBody] SalesDto saleCreate)
        {
            if (saleCreate == null)
                return BadRequest(ModelState);
           

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var saleMap = _mapper.Map<Sales>(saleCreate);
            saleMap.Inventory = _inventoryRepository.GetInventory(inventoryId);

            var inventory = _inventoryRepository.GetInventory(inventoryId);

            if (inventory == null)
            {
                ModelState.AddModelError("", "Inventory not found");
                return NotFound("Inventory not found");
            }

            if (inventory.Quantity <= 0)
            {
                ModelState.AddModelError("", "Insufficient inventory quantity");
                return BadRequest("Insufficient inventory quantity");
            }

           
            inventory.Quantity -= 1;

         
            if (!_inventoryRepository.UpdateInventory(inventory))
            {
                ModelState.AddModelError("", "Something went wrong while updating inventory");
                return StatusCode(500, ModelState);
            }

            if (!_salesRepository.CreateSales(saleMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        
        [HttpPut("{saleid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSales(int saleid, [FromBody] SalesDto updatedSale)
        {
            if (updatedSale == null)
            {
                return BadRequest("Invalid sale data.");
            }
            if (saleid != updatedSale.Id)
            {
                return BadRequest("ID mismatch.");
            }
            if (!_salesRepository.SaleExist(updatedSale.Id))
            {
                return NotFound();
            }
           
            if (!_inventoryRepository.InventoryExist(updatedSale.InventoryID))
            {
                return BadRequest("Invalid SchoolId.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Salemap = _mapper.Map<Sales>(updatedSale);
            if (!_salesRepository.UpdateSales(Salemap))
            {
                ModelState.AddModelError("", "Something went wrong updating Sale");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{saleid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSales( [FromQuery] int inventoryId, int saleid)
        {

            var saleToDelete = _salesRepository.GetSale(saleid);
            if (saleToDelete == null)
            {
                ModelState.AddModelError("", "sale not found");
                return NotFound(ModelState);
            }
            var inventory = _inventoryRepository.GetInventory(inventoryId);

            if (inventory == null)
            {
                ModelState.AddModelError("", "Inventory not found");
                return NotFound("Inventory not found");
            }

            inventory.Quantity += 1;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_salesRepository.DeleteSales(saleToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting sale");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
