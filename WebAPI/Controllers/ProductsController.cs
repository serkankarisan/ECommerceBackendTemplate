using Business.Abstract;
using Core.Utilities.Dynamic;
using Entities.Concrete;
using Entities.DTOs.Products;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        #region Queries
        [HttpGet("get-by-id")]
        public IActionResult GetById(int id)
        {
            var result = _productService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            var products = await _productService.GetAllAsync(index: index, size: size);
            return products.Success ? Ok(products) : BadRequest(products);
        }
        [HttpGet("get-most-expensive-product")]
        public IActionResult GetMostExpensiveProduct()
        {
            return Ok(_productService.GetMostExpensiveProduct());
        }
        [HttpGet("get-product-details-dto")]
        public async Task<IActionResult> GetProductDetailDto(int index, int size)
        {
            var products = await _productService.GetProductDetailDtoAsync(index, size);
            return Ok(products);
        }
        [HttpGet("get-product-details-dto-by-category-id")]
        public async Task<IActionResult> GetProductDetailDtoByCategoryIdAsync(string categoryId, int index, int size)
        {
            var products = await _productService.GetProductDetailDtoByCategoryIdAsync(categoryId, index, size);
            return Ok(products);
        }
        [HttpGet("get-product-details-dto-by-related-category-id")]
        public async Task<IActionResult> GetProductDetailDtoByRelatedCategoryIdAsync(string categoryId, int index, int size)
        {
            var products = await _productService.GetProductDetailDtoByRelatedCategoryIdAsync(categoryId, index, size);
            return Ok(products);
        }
        [HttpGet("get-related-products-by-category-id")]
        public async Task<IActionResult> GetRelatedProductsByCategoryId(string categoryId, int index = 0, int size = 20)
        {
            var products = await _productService.GetRelatedProductsByCategoryId(categoryId, index: index, size: size);
            return Ok(products);
        }
        [HttpGet("get-related-products-by-product-id")]
        public async Task<IActionResult> GetRelatedProductsByProductId(int productId, int index = 0, int size = 20)
        {
            var products = await _productService.GetRelatedProductsByProductId(productId, index: index, size: size);
            return Ok(products);
        }
        [HttpGet("get-products-count-from-dal")]
        public IActionResult GetProductsCountFromDal()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            var result = _productService.GetProductsCountFromDal();
            stopwatch.Stop();
            return Ok($"Total Count :  {result}\nTotal Time : {stopwatch.ElapsedMilliseconds} ms");
        }
        [HttpGet("get-products-count-from-bll")]
        public IActionResult GetProductsCountFromBussines()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            var result = _productService.GetProductsCountFromBussines();
            stopwatch.Stop();
            return Ok($"Total Count :  {result}\nTotal Time : {stopwatch.ElapsedMilliseconds} ms");
        }
        [HttpGet("get-popular-products")]
        public IActionResult GetPopularProducts(int index = 0, int size = 20)
        {
            return Ok(_productService.GetPopularProducts(index, size));
        }
        [HttpGet("get-product-detail-by-id")]
        public IActionResult GetProductDetailByIdAsync(int id)
        {
            return Ok(_productService.GetProductDetailByIdAsync(id));
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public IActionResult Add(AddProductDto addProductDto)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            var result = _productService.Add(addProductDto);
            stopwatch.Stop();
            return result.Success ? Ok($"Total Count : {result}\nTotal Time : {stopwatch.ElapsedMilliseconds} ms") : BadRequest(result);
        }
        [HttpPost("add-range")]
        public IActionResult AddRange(List<Product> products)
        {
            var result = _productService.AddRange(products);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(int productId)
        {
            var result = _productService.Delete(productId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("add-with-image")]
        public IActionResult AddWithImage([FromForm] AddProductWithImageDto addProductWithImageDto)
        {
            var result = _productService.AddWithImage(addProductWithImageDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("get-product-details-paginate-dynamic")]
        public IActionResult GetProductDetailsPaginateDynamic(int index, int size, [FromBody] Dynamic dynamic)
        {
            var products = _productService.GetListDynamic(index, size, dynamic);
            return Ok(products);
        }
        #endregion
    }
}

