using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using APIStoreManagement.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.Metrics;

namespace APIStoreManagement.Contoroller
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CostController : Controller
    {
        private readonly ICostsRepository _costsRepository;
        private readonly IMapper _mapper;
        public CostController(ICostsRepository costsRepository, IMapper mapper)
        {
            _costsRepository = costsRepository;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Costs>))]
        public IActionResult GetCosts()
        {

            var costs = _mapper.Map<List<CostDto>>(_costsRepository.GetCosts());
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(costs);

        }

        [HttpGet("{costid}")]
        [ProducesResponseType(200, Type = typeof(Costs))]
        [ProducesResponseType(400)]
        public IActionResult GetCost(int id)
        {

            var cost = _mapper.Map<CostDto>(_costsRepository.GetCost(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(cost);
        }
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCosts([FromBody] CostDto costCreate)
        {
            if (costCreate == null)
                return BadRequest(ModelState);

            var cost = _costsRepository.GetCosts() .Where(c => c.Title.Trim().ToUpper() == costCreate.Title.TrimEnd().ToUpper())
               
                .FirstOrDefault();

            if (cost != null)
            {
                ModelState.AddModelError("", "cost already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var costMap = _mapper.Map<Costs>(costCreate);

            if (!_costsRepository.CreateCosts(costMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
        [AllowAnonymous]
        [HttpPut("{costid}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCosts(int costid, [FromBody] CostDto updatedCost)
        {
            if (updatedCost == null)
                return BadRequest(ModelState);

            if (costid != updatedCost.Id)
                return BadRequest(ModelState);

            if (!_costsRepository.CostExist(costid))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var sizeMap = _mapper.Map<Costs>(updatedCost);

            if (!_costsRepository.UpdateCosts(sizeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating cost");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
        [HttpDelete("{costId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCosts(int costId)
        {

            if (!_costsRepository.CostExist(costId))
            {
                return NotFound();
            }
            var costToDelet = _costsRepository.GetCost(costId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_costsRepository.DeleteCosts(costToDelet))
            {
                ModelState.AddModelError("", "Something went wrong deleting cost");
            }

            return NoContent();

        }
    }
}
