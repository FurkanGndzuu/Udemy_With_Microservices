using DiscountService.API.Abstarctions;
using DiscountService.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesShared;
using ServicesShared.Services;

namespace DiscountService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : BasesController
    {
        readonly ISharedIdentityService _identityService;
        readonly IDiscountService _discountService;

        public DiscountsController(ISharedIdentityService identityService, IDiscountService discountService)
        {
            _identityService = identityService;
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscounts() => 
            CreateActionResultInstance(await _discountService.GetAllDiscounts());
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetDiscountById([FromRoute] int Id) =>
            CreateActionResultInstance(await _discountService.GetDiscountsById(Id));
        [HttpGet("[action]")]
        public async Task<IActionResult> GetDiscountByCodeAndId([FromQuery] string Code ) =>
            CreateActionResultInstance(await _discountService.GetDiscountsByCodeAndUserId(Code, User.Claims.Where(x => x.Type == "sub").FirstOrDefault().ToString().Split(' ')[1]));

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDiscount(int Id) =>
         CreateActionResultInstance(await _discountService.DeleteDiscount( Id));
        [HttpPost]
        public async Task<IActionResult> SaveDiscount([FromBody]DiscountDTO discount)
        {
            discount.CreatedDate = DateTime.UtcNow;
          
            return CreateActionResultInstance(await _discountService.SaveDiscount(discount));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] DiscountDTO discount)
        {
            return CreateActionResultInstance(await _discountService.UpdateDiscount(discount));
        }

    }
}
