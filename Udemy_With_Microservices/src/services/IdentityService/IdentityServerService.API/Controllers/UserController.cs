using IdentityServerService.API.DTOs;
using IdentityServerService.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServicesShared;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServerService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDTO signUp)
        {
            var user = new ApplicationUser()
            {
                UserName = signUp.UserName,
                Email = signUp.Email,
            };
           IdentityResult result = await _userManager.CreateAsync(user,signUp.Password);

            if (!result.Succeeded)
                return BadRequest(Response<NoContent>.Fail(Errors: result.Errors.Select(x => x.Description).ToList(),
                    StatusCode: StatusCodes.Status400BadRequest));

            return NoContent();
        }
    }
}
