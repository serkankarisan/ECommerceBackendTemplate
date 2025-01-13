using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        #region Queries
        [HttpGet("getall")]
        public async Task<IActionResult> GetAllAsync(int index, int size)
        {
            Core.Utilities.Results.IDataResult<Core.Utilities.Paging.Paginate<CategoryDto>> result = await _categoryService.GetAllAsync(index: index, size: size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-child-categories-by-category-id")]
        public async Task<IActionResult> GetChildCategoriesByCategoryId(int categoryId)
        {
            Core.Utilities.Results.IDataResult<List<CategoryDto>> result = await _categoryService.GetChildCategoriesByCategoryId(categoryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int categoryId)
        {
            Core.Utilities.Results.IDataResult<CategoryDto> result = await _categoryService.GetAsync(q => q.Id == categoryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public IActionResult Add(AddCategoryDto addCategoryDto)
        {
            Core.Utilities.Results.IDataResult<CategoryDto> result = _categoryService.AddWithDto(addCategoryDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Category category)
        {
            Core.Utilities.Results.IResult result = _categoryService.Update(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(int categoryId)
        {
            Core.Utilities.Results.IResult result = _categoryService.Delete(categoryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("add-categories-api")]
        public async Task<IActionResult> AddCategoriesApi()
        {
            //sadece ihtiyac duyulduğunda çalıştırılması gerek
            //return null;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.trendyol.com/sapigw/product-categories");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                dynamic apiRoot = JsonSerializer.Deserialize<dynamic>(responseContent);
                List<dynamic> apiCategories = apiRoot.categories;
                try
                {
                    foreach (dynamic category in apiCategories)
                    {
                        Core.Utilities.Results.IDataResult<CategoryDto> parentResult = _categoryService.AddWithDto(new AddCategoryDto { Name = category.name, ParentCategoryId = null });
                        if (parentResult != null)
                        {

                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else { }
            return null;

        }
        #endregion
    }
}
