using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.CQRS.Commands.CreateOrderC;
using OrderService.Application.CQRS.Queries.GetOrderByUserId;
using ServicesShared;
using ServicesShared.Services;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BasesController
    {
        readonly IMediator _mediator;
        readonly ISharedIdentityService _sharedIdetityService;

        public OrdersController(ISharedIdentityService sharedIdetityService, IMediator mediator)
        {
            _sharedIdetityService = sharedIdetityService;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersByUserId()
        {
          var response =await _mediator.Send(new GetOrderByUserIdQueryRequest() { userId = User.Claims.Where(x => x.Type == "sub").FirstOrDefault().ToString().Split(' ')[1] });

            return CreateActionResultInstance(response);
        }

        [HttpPost]

        public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
        {
            
            var response = await _mediator.Send(request);

            return CreateActionResultInstance(response);   
        }
    }
}
