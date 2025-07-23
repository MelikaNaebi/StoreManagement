using APIStoreManagement.Dto;
using APIStoreManagement.Interfaces;
using APIStoreManagement.Models;
using APIStoreManagement.Repository;
using APIStoreManagement.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIStoreManagement.Contoroller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly IMapper _mapper;

        public SaleController(IMapper mapper, ISaleService saleService)
        {
            _mapper = mapper;
            _saleService = saleService;
        }
        [HttpGet]
        public async Task<ActionResult<List<SalesDto>>> GetSales(int? PatternId, int? SizeId, DateTime? startDate, DateTime? endDate)
        {
            var sales = await _saleService.GetSalesAsync(PatternId, SizeId, startDate, endDate);
            return Ok(sales);
        }

        [HttpGet("{saleId}")]
        [ProducesResponseType(200, Type = typeof(SalesDto))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SalesDto>> GetSale(int saleId)
        {
            try
            {
                var sale = await _saleService.GetSaleByIdAsync(saleId);
                if (sale == null)
                {
                    return NotFound($"sale with ID {saleId} not found.");
                }
                return Ok(sale);
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
        [HttpPut("{id}")]
        public async Task<ActionResult<SalesDto>> UpdateSale(int id, [FromBody] SalesDto saleDto)
        {
            try {
                var result = await _saleService.UpdateSaleAsync(id, saleDto);
                return Ok(result);
            }
            catch (InvalidOperationException ex) 
            {
                return BadRequest(new { message = ex.Message });
            }

        }
     
        [HttpPost]
        public async Task<IActionResult> Createsale([FromBody] SalesDto salesDto) {
            try
            {
                var createsale = await _saleService.CreateSaleAsync(salesDto);

                return CreatedAtAction(nameof(GetSales), new { id = createsale.Id }, createsale);
            }

            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // خطاهای عمومی دیگر
                return StatusCode(500, new { message = "خطا در ثبت فروش: " + ex.Message });
            }
        }

        [HttpDelete("{saleId}")]
    
        public async Task<IActionResult> DeleteSale(int saleId)
        {

            await _saleService.DeleteSaleAsync(saleId);
            return NoContent();

        }

    }
}
