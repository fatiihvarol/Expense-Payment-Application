using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost]
    public async Task<ApiResponse<TokenResponse>> Post([FromBody] TokenRequest request)
    {
        var operation = new CreateTokenCommand(request);
        var result = await _mediator.Send(operation);
        return result;
    }
}