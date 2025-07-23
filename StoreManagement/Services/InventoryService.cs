using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIStoreManagement.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<InventoryDto> CreateInventoryAsync(InventoryCreateUpdateDto inventoryDto)
        {
            var existingInventory = await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(inventoryDto.ClothingId);
            if (existingInventory != null)
            {
                throw new InvalidOperationException($"Inventory for Clothing ID {inventoryDto.ClothingId} already exists. Use Update instead.");
            }

            var inventory = _mapper.Map<Inventory>(inventoryDto);
            await _unitOfWork.Inventorys.CreateInventoryAsync(inventory);
            if (await _unitOfWork.CompleteAsync() <= 0) // CompleteAsync() تعداد رکوردهای ذخیره شده را برمی‌گرداند.
            {
                throw new Exception("Something went wrong when creating inventory");
            }

            var createdInventory = await _unitOfWork.Inventorys.GetInventoryAsync(inventory.Id);
            return _mapper.Map<InventoryDto>(createdInventory);
        }

        public async Task DeleteInventoryAsync(int inventoryId)
        {
            var inventoryToDelete = await _unitOfWork.Inventorys.GetInventoryAsync(inventoryId);
            if (inventoryToDelete == null)
            {
                throw new KeyNotFoundException($"Inventory with ID {inventoryId} not found.");
            }

            await _unitOfWork.Inventorys.DeleteInventoryAsync(inventoryToDelete);
            if (await _unitOfWork.CompleteAsync() <= 0)
            {
                throw new Exception("Something went wrong when deleting inventory");
            }
        }

        public async Task<List<InventoryDto>> GetInventoriesAsync(int? patternId = null)
        {
            var inventories = await _unitOfWork.Inventorys.GetInventoriesAsync(patternId);
            return _mapper.Map<List<InventoryDto>>(inventories);
        }

        public async Task<InventoryDto> GetInventoryAsync(int inventoryId)
        {
            var inventory = await _unitOfWork.Inventorys.GetInventoryAsync(inventoryId);
            if (inventory == null) return null;
            return _mapper.Map<InventoryDto>(inventory);
        }

        public async Task<InventoryDto> GetInventoryByClothingIdAsync(int clothingId)
        {
            var inventory = await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(clothingId);
            if (inventory == null) return null;
            return _mapper.Map<InventoryDto>(inventory);
        }

        public async Task<InventoryDto> UpdateInventoryAsync(int inventoryId, InventoryCreateUpdateDto inventoryDto)
        {
            var existingInventory = await _unitOfWork.Inventorys.GetInventoryAsync(inventoryId);
            if (existingInventory == null)
            {
                throw new KeyNotFoundException($"Inventory with ID {inventoryId} not found.");
            }

            if (existingInventory.ClothingId != inventoryDto.ClothingId)
            {
             
                if (await _unitOfWork.Inventorys.GetInventoryByClothingIdAsync(inventoryDto.ClothingId) != null)
                {
                    throw new InvalidOperationException($"Inventory already exists for Clothing ID {inventoryDto.ClothingId}. Cannot assign to existing inventory record.");
                }
            }

            _mapper.Map(inventoryDto, existingInventory);
            existingInventory.Id = inventoryId;

            await _unitOfWork.Inventorys.UpdateInventoryAsync(existingInventory);
            if (await _unitOfWork.CompleteAsync() <= 0)
            {
                throw new Exception("Something went wrong when updating inventory");
            }

            var updatedInventory = await _unitOfWork.Inventorys.GetInventoryAsync(inventoryId);
            return _mapper.Map<InventoryDto>(updatedInventory);
        }
    }
}