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
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
       // [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<EmployeeResponse>>> GetAllEmployees()
        {
            var operation = new GelAllEmployeesQuery();
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
      //  [Authorize(Roles = "admin")]
        public async Task<ApiResponse<EmployeeResponse>> GetEmployeeById(int id)
        {
            var operation = new GetByIdEmployeeQuery(id) ;
            var result = await _mediator.Send(operation);
            return result;
        }

        [HttpGet("search")]
       // [Authorize(Roles = "admin")]
        public async Task<ApiResponse<List<EmployeeResponse>>> GetEmployeeByParameters(
           [FromQuery] string? identityNumber, [FromQuery] string? firstName    , [FromQuery] string? lastName)
        {
            var operation = new GetByParameterEmployeeQuery( identityNumber,firstName,lastName) ;

            var result = await _mediator.Send(operation);
            return result;
        }
        [HttpPost]
      //  [Authorize(Roles = "admin")]
        public async Task<ApiResponse<EmployeeResponse>> CreateEmployee( EmployeeRequest request)
        {
            var operation = new CreateEmployeeCommand(request) ;
            var result = await _mediator.Send(operation);
            return result;
        }
        [HttpPut("Id")]
       // [Authorize(Roles = "admin")]
        public async Task<ApiResponse> UpdateEmployee(int id,EmployeeRequest request)
        {
            var operation = new UpdateEmployeeCommand(id,request) ;
            var result = await _mediator.Send(operation);
            return result;
        }
        [HttpDelete("Id")]
       // [Authorize(Roles = "admin")]
        public async Task<ApiResponse> DeleteEmployee(int id)
        {
            var operation = new DeleteEmployeeCommand(id) ;
            var result = await _mediator.Send(operation);
            return result;
        }

     
    }
}