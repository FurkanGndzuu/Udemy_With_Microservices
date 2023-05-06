using CatalogService.API.DTOs.Categories;
using ServicesShared;

namespace CatalogService.API.Abstractions
{
    public interface ICategoryService
    {

         Task<Response<List<GetAllCategoryDTO>>> GetAllCategoriesAsync();
         Task<Response<List<GetCategoriesByIdDTO>>> GetCategoriesByIdAsync(string Id);
         Task<Response<NoContent>> CreateCategoryAsync(CreateCategoryDTO categoryDto);
         Task<Response<NoContent>> DeleteCategoryById(string Id);    
    }
}
