using BasketService.API.Abstractions;
using BasketService.API.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesShared;
using ServicesShared.Services;

namespace BasketService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : BasesController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var userClaims = User.Claims.Where(x => x.Type == "sub").FirstOrDefault();
            return CreateActionResultInstance(await _basketService.GetBasket(userClaims.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket([FromBody]BasketDTO basketDto)
        {
            var response = await _basketService.SaveOrUpdate(basketDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()

        {
            var userClaims = User.Claims.Where(x => x.Type == "sub").FirstOrDefault();
            return CreateActionResultInstance(await _basketService.Delete(userClaims.ToString()));
        }
    }
}
