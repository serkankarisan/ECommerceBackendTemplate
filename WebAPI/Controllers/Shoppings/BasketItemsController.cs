using Business.Abstract;
using Business.Abstract.Shoppings;
using Core.Utilities.Results;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Shopping
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketItemsController : ControllerBase
    {
        private readonly IBasketItemService _basketItemService;
        public BasketItemsController(IBasketItemService basketItemService)
        {
            _basketItemService = basketItemService;
        }
        #region Queries
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            var result = await _basketItemService.GetAllAsync(index, size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _basketItemService.GetByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-basket-items-by-user-id")]
        public IActionResult GetBasketItemsByIdUserId(int userId)
        {
            return Ok(_basketItemService.GetBasketItemsByIdUserId(userId));
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(AddBasketItemDto basketItemDto)
        {
            var result = await _basketItemService.AddAsync(basketItemDto);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(BasketItem basketItem)
        {
            var result = await _basketItemService.UpdateAsync(basketItem);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _basketItemService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
    }
}
