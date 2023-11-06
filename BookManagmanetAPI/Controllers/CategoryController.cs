using BusinessLogic.Models.Categories;
using BusinessLogic.Services.CategoryService;
using Microsoft.AspNetCore.Mvc;

namespace BookManagmanetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequestModel model) 
        {
            try
            {
                await _categoryService.Create(model);
                return Ok("Category Has Been successfully created!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            try
            {
                var category = await _categoryService.GetById(id);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var categories = await _categoryService.GetAll();
                return Ok(categories);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletById(Guid id)
        {
            try
            {
                await _categoryService.DeleteById(id);
                return Ok("Success"); 

            }
            catch(Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
        
    }
}
