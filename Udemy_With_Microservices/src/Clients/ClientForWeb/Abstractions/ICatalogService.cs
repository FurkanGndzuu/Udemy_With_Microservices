using ClientForWeb.DTOs;
using ClientForWeb.Models;

namespace ClientForWeb.Abstractions
{
    public interface ICatalogService
    {
        Task<List<CategoryViewModel>> GetAllCategories();
        Task<bool> CreateCategory(CategoryViewModel category);
        Task<bool> DeleteCategory(string Id);

        Task<List<CourseViewModel>> GetAllCourse(); 
        Task<CourseViewModel> GetCourseById(string Id);
        Task<List<CourseViewModel>> GetCoursesByUserId(string userId);

        Task<bool> CreateCourse(CreateCourseDTO course);
        Task<bool> DeleteCourse(string CourseId);
        Task<bool> UpdateCourseAsync(CourseUpdateDTO courseUpdateInput);
    }
}
