using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using APIStoreManagement.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : Controller
    {
        private readonly ISizeRepository _sizeRepository;
        private readonly IMapper _mapper;
        public SizesController( ISizeRepository sizeRepository ,IMapper mapper)
        {
            _sizeRepository = sizeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Sizes>))]
        public IActionResult GetSizes()
        {

            var sizes = _mapper.Map<List<SizesDto>>(_sizeRepository.GetSizes());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(sizes);
        }

        [HttpGet("{sizeid}")]
        [ProducesResponseType(200, Type = typeof(Sizes))]
        [ProducesResponseType(400)]
        public IActionResult GetSize(int id)
        {
            if (!_sizeRepository.SizeExist(id))
                return NotFound();
            var size = _mapper.Map<SizesDto>(_sizeRepository.GetSize(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(size);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] SizesDto sizeCreate)
        {
            if (sizeCreate == null)
                return BadRequest(ModelState);

            var size = _sizeRepository.GetSizes().Where(c => c.SizeName.Trim().ToUpper() == sizeCreate.SizeName.TrimEnd().ToUpper())
                .FirstOrDefault();


            if (size != null)
            {
                ModelState.AddModelError("", "size already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var sizeMap = _mapper.Map<Sizes>(sizeCreate);

            if (!_sizeRepository.CreateSizes(sizeMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{sizeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateSizes(int sizeId, [FromBody] SizesDto updatedsize)
        {
            if (updatedsize == null)
                return BadRequest(ModelState);

            if (sizeId != updatedsize.Id)
                return BadRequest(ModelState);

            if (!_sizeRepository.SizeExist(sizeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var SizeMap = _mapper.Map<Sizes>(updatedsize);

            if (!_sizeRepository.UpdateSizes(SizeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating size");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{sizeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteSizes(int sizeId)
        {

            if (!_sizeRepository.SizeExist(sizeId))
            {
                return NotFound();
            }
            var sizeToDelete = _sizeRepository.GetSize(sizeId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_sizeRepository.DeleteSizes(sizeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting size");
            }

            return NoContent();

        }
    }
}
