using CatalogService.API.DTOs.Courses;
using ServicesShared;

namespace CatalogService.API.Abstractions
{
    public interface ICourseService
    {
        Task<Response<NoContent>> CreateCourseAsync(CreateCourseDTO createCourse);
        Task<Response<List<CourseDTO>>> GetAllCoursesAsync();
        Task<Response<CourseDTO>> GetCoursesByIdAsync(string Id);
        Task<Response<NoContent>> UpdateCourseAsync(CourseUpdateDTO updateCourse);
        Task<Response<List<CourseDTO>>> GetCoursesByUserIdAsync(string userId);
        Task<Response<NoContent>> DeleteCourseById(string Id);
    }
}
