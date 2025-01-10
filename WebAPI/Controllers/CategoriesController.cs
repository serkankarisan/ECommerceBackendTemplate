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
            var result = await _categoryService.GetAllAsync(index: index, size: size);
            return result.Success ? Ok(result) : BadRequest();
        }
        [HttpGet("get-child-categories-by-category-id")]
        public async Task<IActionResult> GetChildCategoriesByCategoryId(string categoryId)
        {
            var result = await _categoryService.GetChildCategoriesByCategoryId(categoryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        #endregion
        #region Commands
        [HttpPost("add")]
        public IActionResult Add(AddCategoryDto addCategoryDto)
        {
            var result = _categoryService.AddWithDto(addCategoryDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("update")]
        public IActionResult Update(Category category)
        {
            var result = _categoryService.Update(category);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("delete")]
        public IActionResult Delete(int categoryId)
        {
            var result = _categoryService.Delete(categoryId);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("add-child-category")]
        public IActionResult AddChildCategory(AddChildCategoryDto addChildCategoryDto)
        {
            var result = _categoryService.AddChildCategory(addChildCategoryDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("add-categories-api")]
        public async Task<IActionResult> AddCategoriesApi()
        {
            //sadece ihtiyac duyulduğunda çalıştırılması gerek
            return null;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://api.trendyol.com/sapigw/product-categories");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                ApiRoot apiRoot = JsonSerializer.Deserialize<ApiRoot>(responseContent);
                List<ApiCategory> apiCategories = apiRoot.categories;
                try
                {
                    foreach (var category in apiCategories)
                    {
                        var parentResult = _categoryService.AddCategoryWithDto(new AddCategoryDto { Name = category.name, ParentCategoryId = "" });
                        if (parentResult != null)
                        {
                            AddChildCategoryDto(category.subCategories, parentResult.CategoryId);
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
        private void AddChildCategoryDto(List<ApiCategory> subCategories, string categoryId)
        {
            foreach (var category in subCategories)
            {
                var result = _categoryService.AddCategoryWithDto(new AddCategoryDto { Name = category.name, ParentCategoryId = categoryId });
                foreach (var childCategory in category.subCategories)
                {
                    if (childCategory.subCategories != null)
                    {
                        AddChildCategoryDto(childCategory.subCategories, result.CategoryId);
                    }
                }
            }
        }

        #endregion
    }
    public class ApiCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public object parentId { get; set; }
        public List<ApiCategory> subCategories { get; set; }
    }

    public class ApiRoot
    {
        public List<ApiCategory> categories { get; set; }
    }

    public class ApiSubCategory
    {
        public int id { get; set; }
        public string name { get; set; }
        public int parentId { get; set; }
        public List<ApiSubCategory> subCategories { get; set; }
    }
}
