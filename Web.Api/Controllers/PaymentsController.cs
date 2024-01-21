using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class PaymentsController
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<PaymentResponse>>> GetAllPayments()
    {
        var operation = new GelAllPaymentsQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")] 
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<PaymentResponse>> GetPaymentById(int id)
    {
        var operation = new GetByIdPaymentQuery(id) ;
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("search")] 
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<PaymentResponse>>> GetPaymentsByParameters(
        [FromQuery] int expenseId,[FromQuery] decimal amount,[FromQuery] string receiverIban)
    {
        var operation = new GetByParameterPaymentsQuery(expenseId,receiverIban,amount) ;

        var result = await _mediator.Send(operation);
        return result;
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