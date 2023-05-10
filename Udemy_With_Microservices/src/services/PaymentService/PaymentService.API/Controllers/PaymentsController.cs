using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServicesShared;

namespace PaymentService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : BasesController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(Response<NoContent>.Success(StatusCodes.Status200OK));
        }
    }
}
