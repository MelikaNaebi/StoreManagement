using APIStoreManagement.Dto;
using APIStoreManagement.DTOs;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using APIStoreManagement.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClothingController : Controller
    {
        private readonly IClothingRepository _clothingRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IPatternRepository _patternRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ClothingController(IClothingRepository clothingRepository,DataContext context, IMapper mapper, IPatternRepository schoolRepository, ISizeRepository sizeRepository, IInventoryRepository inventoryRepository)
        {
            _clothingRepository = clothingRepository;
            _mapper = mapper;
            _patternRepository = schoolRepository;
            _sizeRepository = sizeRepository;
            _inventoryRepository = inventoryRepository;
            _context = context;
        }
        //finding clothingid by size & pattern
        [HttpPost("get-by-size-and-pattern")]
        public async Task<IActionResult> GetClothingBySizeAndPattern([FromBody] ClothingSearchRequest request)
        {
            var clothing = await _context.Clothings
                .Include(c => c.Size)
                .Include(c => c.Pattern)
                .FirstOrDefaultAsync(c => c.SizeId == request.SizeId && c.PatternId == request.PatternId);

            if (clothing == null)

                return NotFound(new { message = "لباسی با این سایز و مدرسه یافت نشد." });
            return Ok(new
            {
                clothing.Id,
                clothing.Price,
                Size = clothing.Size.SizeName,
                Pattern = clothing.Pattern.Name
            });
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Clothing>))]
        public IActionResult GetClothings()
        {

            var clothings = _mapper.Map<List<ClothingDto>>(_clothingRepository.GetClothings());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(clothings);
        }

        [HttpGet("{clothingid}")]
        [ProducesResponseType(200, Type = typeof(Clothing))]
        [ProducesResponseType(400)]
        public IActionResult GetClothing(int id)
        {
            if (!_clothingRepository.ClothingExist(id))
                return NotFound();
            var clothing =_mapper.Map<ClothingDto>(_clothingRepository.GetClothing(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(clothing);
        }

        [HttpGet("ClothingsBySchoolAndSize/{schoolId}/{sizeId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClothingDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetClothingsBySchoolAndSize(int schoolId, int sizeId)
        {
            var clothings = _mapper.Map<List<ClothingDto>>(_clothingRepository.GetClothingsByPatternAndSize(schoolId, sizeId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(clothings);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateClothing([FromQuery] int patternId, [FromQuery] int sizeId, [FromBody]ClothingDto clothingCreate)
        {
            if (clothingCreate == null)
            {
                return BadRequest(ModelState);
            }

            var clothing = _clothingRepository.GetClothings().Where(c => c.Id == clothingCreate.Id).FirstOrDefault();

            if (clothing != null)
            {
                ModelState.AddModelError("", "Clothing already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var clothingMap = _mapper.Map<Clothing>(clothingCreate);
            clothingMap.Pattern = _patternRepository.GetPattern(patternId);
            clothingMap.Size = _sizeRepository.GetSize(sizeId);


            if (!_clothingRepository.CreateClothing(clothingMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{clothingid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateClothing(int clothingid, [FromBody] ClothingDto updatedclothing)
        {
            if (updatedclothing == null)
            {
                return BadRequest("Invalid clothing data.");
            }
            if (clothingid != updatedclothing.Id)
            {
                return BadRequest("ID mismatch.");
            }
            if (!_clothingRepository.ClothingExist(updatedclothing.Id))
            {
                return NotFound();
            }
            if (!_sizeRepository.SizeExist(updatedclothing.SizeId))
            {
                return BadRequest("Invalid SizeId.");
            }
            if (!_patternRepository.PatternExist(updatedclothing.PatternId))
            {
                return BadRequest("Invalid SchoolId.");
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var clothingmap = _mapper.Map<Clothing>(updatedclothing);
            if (!_clothingRepository.UpdateClothing(clothingmap))
            {

                ModelState.AddModelError("", "Something went wrong updating clothing");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }
        [HttpDelete("{clothingId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteClothing(int clothingId)
        {
            
            var clothingToDelete = _clothingRepository.GetClothing(clothingId);
            if (clothingToDelete == null)
            {
                ModelState.AddModelError("", "Clothing not found");
                return NotFound(ModelState);
            }

            // دریافت رکوردهای Inventory مرتبط
            //var inventoriesToDelete = _inventoryRepository.GetInventoriesByClothingId(clothingId);
            //if (inventoriesToDelete != null && inventoriesToDelete.Any())
            //{
            //    foreach (var inventory in inventoriesToDelete)
            //    {
            //        if (!_inventoryRepository.DeleteInventory(inventory))
            //        {
            //            ModelState.AddModelError("", "Something went wrong when deleting inventory");
            //            return StatusCode(500, ModelState);
            //        }
            //    }
            //}

           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

       
            if (!_clothingRepository.DeleteClothing(clothingToDelete))
            {
                ModelState.AddModelError("", "Something went wrong when deleting clothing");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}