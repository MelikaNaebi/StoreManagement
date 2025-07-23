using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<InventoryDto>))]
        public async Task<ActionResult<List<InventoryDto>>> GetInventories([FromQuery ] int? patternId )
        {
            var inventories = await _inventoryService.GetInventoriesAsync(patternId);
            return Ok(inventories);
        }

        [HttpGet("{inventoryId}")]
        [ProducesResponseType(200, Type = typeof(InventoryDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<InventoryDto>> GetInventory(int inventoryId)
        {
            try
            {
                var inventory = await _inventoryService.GetInventoryAsync(inventoryId);
                if (inventory == null)
                {
                    return NotFound($"Inventory with ID {inventoryId} not found.");
                }
                return Ok(inventory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("byClothing/{clothingId}")]
        [ProducesResponseType(200, Type = typeof(InventoryDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<InventoryDto>> GetInventoryByClothingId(int clothingId)
        {
            try
            {
                var inventory = await _inventoryService.GetInventoryByClothingIdAsync(clothingId);
                if (inventory == null)
                {
                    return NotFound($"Inventory for Clothing ID {clothingId} not found.");
                }
                return Ok(inventory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(InventoryDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> CreateInventory([FromBody] InventoryCreateUpdateDto inventoryCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdInventory = await _inventoryService.CreateInventoryAsync(inventoryCreateDto);
                return CreatedAtAction(nameof(GetInventory), new { inventoryId = createdInventory.Id }, createdInventory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{inventoryId}")]
        [ProducesResponseType(200, Type = typeof(InventoryDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        public async Task<IActionResult> UpdateInventory(int inventoryId, [FromBody] InventoryCreateUpdateDto updatedInventoryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedInventory = await _inventoryService.UpdateInventoryAsync(inventoryId, updatedInventoryDto);
                return Ok(updatedInventory);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{inventoryId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteInventory(int inventoryId)
        {
            try
            {
                await _inventoryService.DeleteInventoryAsync(inventoryId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}