


//using Microsoft.AspNetCore.Mvc;

//[Route("api/[controller]")]
//[ApiController]
//public class PayPalController : ControllerBase
//{
//    private readonly PayPalService _payPalService;

//    public PayPalController(PayPalService payPalService)
//    {
//        _payPalService = payPalService;
//    }

//    [HttpPost("create-payment")]
//    public IActionResult CreatePayment([FromBody] PaymentRequest request)
//    {
//        var payment = _payPalService.CreatePayment("http://localhost:4200", request.Amount);  // /paymentsuccess
//        return Ok(new { paymentId = payment.id, links = payment.links });
//    }

//    public class PaymentRequest
//    {
//        public decimal Amount { get; set; }
//    }
//}
