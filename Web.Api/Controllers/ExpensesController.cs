using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ExpensesController:ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMemoryCache _memoryCache;

    public ExpensesController(IMediator mediator,IMemoryCache memoryCache)
    {
        _mediator = mediator;
        _memoryCache = memoryCache;
    }

    
    [HttpGet("MyExpenses")]
    [Authorize]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetMyProfile()
    {
        if (!int.TryParse(User.FindFirstValue("Id"), out var userId))
        {
            return new ApiResponse<List<ExpenseResponse>>("Invalid user information");
        }

        var cacheKey = $"ApplicationUserId_{userId}";
        if (_memoryCache.TryGetValue(cacheKey, out int cachedEmployeeId))
        {
            var result = await _mediator.Send(new GelExpenseByEmployeeIdQuery(cachedEmployeeId));
            return result;
        }

        return new ApiResponse<List<ExpenseResponse>>("Employee information not found in cache");
    }
    [HttpGet]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetAllEmployees()
    {
        var operation = new GelAllExpensesQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    //  [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ExpenseResponse>> GetEmployeeById(int id)
    {
        var operation = new GetByIdExpenseQuery(id) ;
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("search")]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<ExpenseResponse>>> GetEmployeeByParameters(
        [FromQuery] int categoryId, [FromQuery] int employeeId    , [FromQuery] string? status,[FromQuery] decimal amount)
    {
        var operation = new GetByParameterExpenseQuery(categoryId,employeeId,status,amount) ;

        var result = await _mediator.Send(operation);
        return result;
    }
    
    
    [HttpPost]
    //  [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ExpenseResponse>> CreateExpense( ExpenseRequest request)
    {
        var operation = new CreateExpenseCommand(request) ;
        var result = await _mediator.Send(operation);
        return result;
    }
    
    [HttpPost("Reject/{id}")]
    //  [Authorize(Roles = "admin")]
    public async Task<ApiResponse> DeclineExpense( int id ,string rejectionDescription)
    {
        var operation = new DeclineExpenseCommand(id,rejectionDescription) ;
        var result = await _mediator.Send(operation);
        return result;
    }
    [HttpPut("Id")]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse> UpdateExpense(int id,[FromQuery] string description)
    {
        var operation = new UpdateExpenseCommand(id,description) ;
        var result = await _mediator.Send(operation);
        return result;
    }
    [HttpDelete("Id")]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse> DeleteExpense(int id)
    {
        var operation = new DeleteExpenseCommand(id) ;
        var result = await _mediator.Send(operation);
        return result;
    }
}