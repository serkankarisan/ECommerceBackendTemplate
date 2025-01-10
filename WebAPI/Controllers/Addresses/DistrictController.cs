using Business.Abstract.Addresses;
using Entities.Concrete.AddressConcrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;
        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }
        #region Queries
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            var result = await _districtService.GetAllAsync(index, size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _districtService.GetByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-county-id")]
        public async Task<IActionResult> GetByCountyIdAsync(int index, int size, string countyId)
        {
            var result = await _districtService.GetByCountyIdAsync(index: index, size: size, countyId: countyId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(District district)
        {
            var result = await _districtService.AddAsync(district);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(District district)
        {
            var result = await _districtService.UpdateAsync(district);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _districtService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion

        [HttpGet("getall-from-bussines")]
        public IActionResult GetAllFromBussines()
        {
            return Ok(_districtService.GetAllFromBussiness());
        }
        [HttpGet("getall-from-dal")]
        public IActionResult GetAllFromDal()
        {
            return Ok(_districtService.GetAllFromDal());

        }

    }
}