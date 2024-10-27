using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using APIStoreManagement.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController :Controller
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IClothingRepository _clothingRepository1;
      
        private readonly IMapper _mapper;
        public InventoryController(IInventoryRepository inventoryRepository,IMapper mapper, IClothingRepository clothingRepository, IClothingRepository clothingRepository1, IPatternRepository schoolRepository, ISizeRepository sizeRepository)
        {
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
            _clothingRepository1 = clothingRepository1;
       
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Inventory>))]
        public IActionResult GetInventorys() {

            var inventories = _mapper.Map <List<InventoryDto >> (_inventoryRepository.GetInventorys());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(inventories);
        }
        [HttpGet("inventoryid")]
        [ProducesResponseType(200, Type = typeof(Inventory))]
        [ProducesResponseType(400)]
        public IActionResult GetInventory(int id)
        {
            var inventory=_mapper.Map<InventoryDto>(_inventoryRepository.GetInventory(id));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(inventory);
        }

        [HttpGet("clothingid")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<InventoryDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetInventoriesByClothingId(int clothingid)
        {
            var inventories=_mapper.Map<List<InventoryDto>>(_inventoryRepository.GetInventoriesByClothingId(clothingid));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(inventories);
        }
       

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateInventory( [FromQuery] int clothingId,[FromBody] InventoryDto inventoryCreate)
        {
            if (inventoryCreate == null)
                return BadRequest(ModelState);

            var inventory = _inventoryRepository.GetInventorys()
                .Where(c => c.ClothingId == inventoryCreate.ClothingId).FirstOrDefault();


            if (inventory != null)
            {
                ModelState.AddModelError("", "Inventory already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventoryMap = _mapper.Map<Inventory>(inventoryCreate);
            inventoryMap.Clothing = _clothingRepository1.GetClothing(clothingId);
         
            if (!_inventoryRepository.CreateInventory(inventoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{inventoryid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateInventory(int inventoryid, [FromBody] InventoryDto updatedInventory)

        {
            if (updatedInventory == null)
            {
                return BadRequest("Invalid inventory data.");
            }
            if (inventoryid != updatedInventory.Id)
            {
                return BadRequest("ID mismatch.");
            }
            if (!_inventoryRepository.InventoryExist(updatedInventory.Id))
            {
                return NotFound();
            }
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Inventorymap = _mapper.Map<Inventory>(updatedInventory);
            if (!_inventoryRepository.UpdateInventory(Inventorymap))
            {

                ModelState.AddModelError("", "Something went wrong updating inventory");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
        [HttpDelete("{inventoryid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteInventory(int inventoryid)
        {

            var inventoryToDelete = _inventoryRepository.GetInventory(inventoryid);
            if (inventoryToDelete == null)
            {
                ModelState.AddModelError("", "Inventory not found");
                return NotFound(ModelState);
            }

   
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (!_inventoryRepository.DeleteInventory(inventoryToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting Inventory");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
