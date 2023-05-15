using ClientForWeb.Abstractions;
using ClientForWeb.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServicesShared.Services;

namespace ClientForWeb.Controllers
{
    public class CatalogsController : Controller
    {
        readonly ICatalogService _catalogService;
        readonly ISharedIdentityService _sharedIdentityService;

        public CatalogsController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetCoursesByUserId(_sharedIdentityService.UserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategories();

            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDTO courseCreateInput)
        {

            var categories = await _catalogService.GetAllCategories();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            courseCreateInput.UserId = _sharedIdentityService.UserId;

            await _catalogService.CreateCourse(courseCreateInput);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> CreateCategory()
        {
             return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(string Name)
        {
           await _catalogService.CreateCategory(new Models.CategoryViewModel { Name = Name });  

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetCourseById(id);
            var categories = await _catalogService.GetAllCategories();

            if (course == null)
            {
                //mesaj göster
                RedirectToAction(nameof(Index));
            }
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", course.Id);
            CourseUpdateDTO courseUpdateInput = new()
            {
                Id = course.Id,
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Feature = new FeatureDTO() { Duration = course.Feature.Duration},
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture
            };

            return View(courseUpdateInput);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateDTO courseUpdateInput)
        {
            var categories = await _catalogService.GetAllCategories();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdateInput.Id);
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _catalogService.UpdateCourseAsync (courseUpdateInput);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _catalogService.DeleteCourse(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
