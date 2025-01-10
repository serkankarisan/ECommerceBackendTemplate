﻿using Business.Abstract.Addresses;
using Entities.Concrete.AddressConcrete;
using Entities.DTOs.Addresses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Addresses
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        #region Queries
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            var result = await _addressService.GetAllAsync(index, size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("getall-by-user-id")]
        public async Task<IActionResult> GetAllByUserIdAsync(int index, int size, int userId)
        {
            var result = await _addressService.GetAllByUserIdAsync(index, size, userId: userId);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _addressService.GetByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-user-id")]
        public async Task<IActionResult> GetByUserIdAsync(int userId)
        {
            var result = await _addressService.GetByUserIdAsync(userId);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(AddAddressDto addAddressDto)
        {
            var result = await _addressService.AddAsync(addAddressDto);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(Address address)
        {
            var result = await _addressService.UpdateAsync(address);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _addressService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
    }
}