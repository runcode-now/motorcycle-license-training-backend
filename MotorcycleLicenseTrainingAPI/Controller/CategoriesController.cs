using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Service;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getCategoryByType/{type}")]
        public async Task<IActionResult> Get([FromRoute] string type)
        {
            try 
            {
                var categoryList = await _categoryService.GetCategoryByType(type);
                return Ok(categoryList);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {

            return Ok("Get all Category");
        }
    }
}
