using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
  public class PaymentController : BaseController
  {
    private IPaymentService _paymentService;
    public PaymentController(IPaymentService paymentService)
    {
      _paymentService = paymentService;
    }

    [HttpGet("{paymentId}")]
    [Authorize(Roles = "Admin, Customer")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaymentReadDto>> FindOne(Guid paymentId)
    {
      PaymentReadDto? payment = await _paymentService.FindOne(paymentId);
      if (payment is not null)
      {
        return Ok(payment);
      }
      return NotFound();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PaymentReadDto>> CreateOne([FromBody] PaymentCreateDto newPayment)
    {
      return Ok(await _paymentService.CreateOne(newPayment));
    }

    [HttpGet("test")]
    public void Test()
    {
      Response.Redirect("https://secure.PayTabs.com/payment/request");
    }
  }
}