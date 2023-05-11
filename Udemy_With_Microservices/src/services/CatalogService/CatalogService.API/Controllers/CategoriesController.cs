using CatalogService.API.Abstractions;
using CatalogService.API.DTOs.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesShared;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BasesController
    {

        readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            CreateActionResultInstance(await _categoryService.GetAllCategoriesAsync());

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoriesById(string Id) => 
            CreateActionResultInstance(await _categoryService.GetCategoriesByIdAsync(Id));

        [HttpPost]

        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategory) => 
            CreateActionResultInstance(await _categoryService.CreateCategoryAsync(createCategory));

        [HttpDelete]

        public async Task<IActionResult> DeleteCategory(string Id) =>
           CreateActionResultInstance(await _categoryService.DeleteCategoryById(Id));
    }
}
