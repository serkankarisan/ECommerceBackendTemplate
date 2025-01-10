using Business.Abstract.Shoppings;
using Entities.Concrete.Shoppings;
using Entities.DTOs.Shoppings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Shopping
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsController : ControllerBase
    {

        private readonly IOrderItemService _orderItemService;
        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        #region Queries
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            var result = await _orderItemService.GetAllAsync(index, size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _orderItemService.GetByIdAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public async Task<IActionResult> AddAsync(AddOrderItemDto addOrderItemDto)
        {
            var result = await _orderItemService.AddAsync(addOrderItemDto);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAsync(OrderItem orderItem)
        {
            var result = await _orderItemService.UpdateAsync(orderItem);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _orderItemService.DeleteAsync(id);
            return result.Success ? Ok(result) : BadRequest();
        }
        #endregion
    }
}
