using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using APIStoreManagement.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Diagnostics.Metrics;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatternController : Controller
    {
        private readonly IPatternRepository _patternlRepository;
        private readonly IMapper _mapper;
        public PatternController(IPatternRepository patternlRepository, IMapper mapper)
        {
            _patternlRepository = patternlRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pattern>))]
        public IActionResult GetPatterns()
        {

            var patterns = _mapper.Map<List<Dto.PatternDto>>(_patternlRepository.GetPatterns());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(patterns);
        }

        [HttpGet("{patternid}")]
        [ProducesResponseType(200, Type = typeof(Pattern))]
        [ProducesResponseType(400)]
        public IActionResult GetPattern(int id)
        {
            if (!_patternlRepository.PatternExist(id))
                return NotFound();
            var pattern = _mapper.Map<Dto.PatternDto>(_patternlRepository.GetPattern(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(pattern);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePattern([FromBody] Dto.PatternDto patternCreate)
        {
            if (patternCreate == null)
                return BadRequest(ModelState);

            var pattern = _patternlRepository.GetPatterns().Where(c => c.Name.Trim().ToUpper() == patternCreate.Name.TrimEnd().ToUpper())

                .FirstOrDefault();

            if (pattern != null)
            {
                ModelState.AddModelError("", "pattern already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patternMap = _mapper.Map<Pattern>(patternCreate);

            if (!_patternlRepository.CreatePattern(patternMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [HttpPut("{patternId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePattern(int patternId, [FromBody] Dto.PatternDto updatedpattern)
        {
            if (updatedpattern == null)
                return BadRequest(ModelState);

            if (patternId != updatedpattern.Id)
                return BadRequest(ModelState);

            if (!_patternlRepository.PatternExist(patternId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var PatternMap = _mapper.Map<Pattern>(updatedpattern);

            if (!_patternlRepository.UpdatePattern(PatternMap))
            {
                ModelState.AddModelError("", "Something went wrong updating Pattern");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{patternId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePattern(int patternId) {

            if (!_patternlRepository.PatternExist(patternId))
            {
                return NotFound();
            }
        var patternToDelet = _patternlRepository.GetPattern(patternId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_patternlRepository.DeletePattern(patternToDelet))
            {
                ModelState.AddModelError("", "Something went wrong deleting pattern");
            }

            return NoContent();

        }
    }
}
