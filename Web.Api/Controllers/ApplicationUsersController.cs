using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;

namespace WebApi.Controllers
{
    using System.Security.Claims;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ApplicationUsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<ApplicationUserResponse>>> Get()
        {
            var operation = new GelAllApplicationUserQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<ApplicationUserResponse>> GetById(int id)
        {
            var operation = new GetByIdApplicationUserQuery(id) ;
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("search")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<ApplicationUserResponse>>> GetByParameters(
            [FromQuery] string? firstName, [FromQuery] string? lastName, [FromQuery] string? email    , [FromQuery] string? role)
        {
            var operation = new GetByParameterApplicationUserQuery(role, email, firstName, lastName) ;

            var result = await _mediator.Send(operation);
            return result;
        }
        [HttpPost] 
        public async Task<ApiResponse<ApplicationUserResponse>> CreateApplicationUser( ApplicationUserRequest request)
        {
            var operation = new CreateApplicationUserCommand(request) ;
            var result = await _mediator.Send(operation);
            return result;
        }
        [HttpPut("Id")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> UpdateApplicationUser(int id,ApplicationUserRequest request)
        {
            var operation = new UpdateApplicationUserCommand(id,request) ;
            var result = await _mediator.Send(operation);
            return result;
        }
        [HttpDelete("Id")]
        [Authorize(Roles = "admin")]
        public async Task<ApiResponse> DeleteApplicationUser(int id)
        {
            var operation = new DeleteApplicationUserCommand(id) ;
            var result = await _mediator.Send(operation);
            return result;
        }

     
    }
}