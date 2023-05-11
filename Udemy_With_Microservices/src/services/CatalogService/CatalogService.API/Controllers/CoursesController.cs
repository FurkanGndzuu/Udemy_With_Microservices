using CatalogService.API.Abstractions;
using CatalogService.API.DTOs.Courses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesShared;

namespace CatalogService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : BasesController
    {
        readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses() => 
            CreateActionResultInstance(await _courseService.GetAllCoursesAsync());
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetCoursesById(string Id) =>
            CreateActionResultInstance(await _courseService.GetCoursesByIdAsync(Id));
        [HttpGet("{userId}")]
        public async Task<IActionResult> GteCoursesById(string userId) =>
            CreateActionResultInstance(await _courseService.GetCoursesByUserIdAsync(userId));
        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO createCourse) =>
            CreateActionResultInstance(await _courseService.CreateCourseAsync(createCourse));
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id) =>
            CreateActionResultInstance(await _courseService.DeleteCourseById(Id));

    }
}
