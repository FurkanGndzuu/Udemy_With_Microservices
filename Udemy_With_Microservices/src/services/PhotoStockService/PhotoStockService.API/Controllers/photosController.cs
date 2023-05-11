using Microsoft.AspNetCore.Mvc;
using PhotoStockService.API.DTOs;
using ServicesShared;

namespace PhotoStockService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class photosController : BasesController
    {
        [HttpPost]
        public async Task<IActionResult> SavePhotos(IFormFile photo,CancellationToken cancellationToken)
        {
            if (photo == null)
                CreateActionResultInstance(Response<NoContent>.Fail("photo is not found", StatusCodes.Status404NotFound));

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
            using var stream = new FileStream(path , FileMode.Create);
            await photo.CopyToAsync(stream);

            var returnPath = photo.FileName;

            PhotoDTO photoDto = new() { path = returnPath };

            return CreateActionResultInstance(Response<PhotoDTO>.Success(photoDto, StatusCodes.Status201Created));
        }

        [HttpDelete]
        public IActionResult DeletePhotos([FromQuery]string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
