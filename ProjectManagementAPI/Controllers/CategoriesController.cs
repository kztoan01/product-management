using BusinessObjects;
using BusinessObjects.Objects;
using Microsoft.AspNetCore.Mvc;
using Services.Category_Service;

namespace ProjectManagementAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();  

            return Ok(category); 
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = new Category
            {
                CategoryName = request.CategoryName
            };

            var createdCate = await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCate.CategoryId }, createdCate);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            category.CategoryName = request.CategoryName;

            var updatedCate =  await _categoryService.UpdateCategoryAsync(category);
            return Ok(updatedCate); 
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deletedCate =  await _categoryService.DeleteCategoryAsync(id);
            if (deletedCate == null)
            {
                return NotFound(ModelState);
            }
            return Ok(deletedCate);  
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchCategories([FromQuery] CategoryQuery query)
        {
            var categories = await _categoryService.SearchCategoriesAsync(query);
            return Ok(categories); 
        }
    }

}
