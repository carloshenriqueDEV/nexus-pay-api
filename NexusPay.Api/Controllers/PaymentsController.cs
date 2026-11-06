using Microsoft.AspNetCore.Mvc;

namespace NexusPay.Api.Controllers
{
    public record ProcessPaymentRequest(string CardNumber, string HolderName, decimal Amount, string Currency);
    public record PaymentResponse(Guid Id, string Status, decimal Amount, DateTime ProcessedAt);
    public record PaymentDetailsResponse(Guid Id, string Status, decimal Amount, string Currency);

    [ApiController]
    [Route("api/v1/payments")]
    public class PaymentsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ProcessPayment([FromBody] ProcessPaymentRequest request)
        {
            await Task.Delay(150);

            var response = new PaymentResponse(Id: Guid.NewGuid(), Status: "Approved", Amount: request.Amount, ProcessedAt: DateTime.UtcNow);

            return CreatedAtAction(nameof(GetPaymentById), new { id = response.Id }, response);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetPaymentById([FromRoute] Guid id)
        {
            var response = new PaymentDetailsResponse(
                    Id: id,
                    Status: "Approved",
                    Amount: 250.00m,
                    Currency: "BRL"
                );
            return Ok(response);
        }
    }

}
