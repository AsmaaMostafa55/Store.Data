using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Store.Service.Services.BasketService.Dtos;
using Store.Service.Services.PaymentService;
using Stripe;

namespace Store.Web.Controllers
{
   
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        const string endpointSecret = " whsec_069023e9b495a64d9f91e5f652873be839b9ff7f25cbe06e5ea681f4c9bfd6e5 ";
        public PaymentController(IPaymentService paymentService,ILogger <PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost]
        public  async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
            =>Ok (await _paymentService.CreateOrUpdatePaymentIntent(input));


        [HttpPost("")]
        public async Task< IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
               endpointSecret
            );
            //PaymentIntent paymentIntent;
            if (stripeEvent.Type == "Payment_intent.Payment_canceled")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Handle the payment intent canceled
                Console.WriteLine($"PaymentIntent canceled: {paymentIntent.Id}");
            }
            else if (stripeEvent.Type == "Payment_intent.Payment_failed")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                _logger.LogInformation("Payment Failed :",paymentIntent.Id);
               var order= await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                _logger.LogInformation("order updated to payment failed :", Order.Id);
            }
           
            else if (stripeEvent.Type == "Payment_intent.Payment_Succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
            
                _logger.LogInformation("Payment Succedd :", paymentIntent.Id);
                var order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                _logger.LogInformation("order updated to payment failed :", Order.Id);
            }
            else if (stripeEvent.Type == "Payment_intent.Payment_created")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Handle the payment intent created
                Console.WriteLine($"PaymentIntent created: {paymentIntent.Id}");
            }

            return Ok();
        }




    }
}
