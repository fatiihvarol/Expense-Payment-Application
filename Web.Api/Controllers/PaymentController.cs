using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentController
{
    private readonly IMediator _mediator;

    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
    
    
    
    
    
    
    
    
    
    
    [HttpPost("Approve")]
    //  [Authorize(Roles = "admin")]
    public async Task<ApiResponse<PaymentResponse>> CreatePayment( int id,[FromQuery] string description)
    {
        var operation = new CreatePaymentCommand(id,description) ;
        var result = await _mediator.Send(operation);
        return result;
    }
    [HttpPut("Id")]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse> UpdatePayment(int id,[FromQuery] string description)
    {
        var operation = new UpdatePaymentCommand(id,description) ;
        var result = await _mediator.Send(operation);
        return result;
    }
    [HttpDelete("Id")]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse> DeletePayment(int id)
    {
        var operation = new DeletePaymentCommand(id) ;
        var result = await _mediator.Send(operation);
        return result;
    }
}