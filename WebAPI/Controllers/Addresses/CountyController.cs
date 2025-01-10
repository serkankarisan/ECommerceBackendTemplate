﻿using Business.Abstract.Addresses;
using Entities.Concrete.AddressConcrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountyController : ControllerBase
    {
        private readonly ICountyService _countyService;
        public CountyController(ICountyService countyService)
        {
            _countyService = countyService;
        }
        #region Queries
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            var result = await _countyService.GetAllAsync(index, size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _countyService.GetByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-city-id")]
        public async Task<IActionResult> GetCountyByCityIdAsync(int index, int size, int cityId)
        {
            var result = await _countyService.GetCountyByCityIdAsync(index: index, size: size, cityId: cityId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(County county)
        {
            var result = await _countyService.AddAsync(county);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(County county)
        {
            var result = await _countyService.UpdateAsync(county);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _countyService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
    }
}