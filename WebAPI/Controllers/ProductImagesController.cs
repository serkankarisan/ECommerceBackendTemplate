using Business.Abstract;
using Entities.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        [HttpPost("add-product-image-by-product-id")]
        public IActionResult AddProductImageByProductId([FromForm] AddProductImageDto addProductImageDto)
        {
            var result = _productImageService.AddProductImageByProductId(addProductImageDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
