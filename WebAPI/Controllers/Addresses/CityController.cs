using Business.Abstract.Addresses;
using Entities.Concrete.AddressConcrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }
        #region Queries
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            var result = await _cityService.GetAllAsync(index, size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _cityService.GetByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(City city)
        {
            var result = await _cityService.AddAsync(city);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(City city)
        {
            var result = await _cityService.UpdateAsync(city);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _cityService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
    }
}