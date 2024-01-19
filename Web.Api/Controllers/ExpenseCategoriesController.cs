using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ExpenseCategoriesController
{
    private readonly IMediator _mediator;

    public ExpenseCategoriesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<ExpenseCategoryResponse>>> GetAlExpenseCategories()
    {
        var operation = new GelAllExpenseCategoriesQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    //  [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ExpenseCategoryResponse>> GetExpenseCategoryById(int id)
    {
        var operation = new GetByIdExpenseCategoryQuery(id) ;
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("search")]
    // [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<ExpenseCategoryResponse>>> GetExpenseCategoryByParameters(
        [FromQuery] string categoryName, [FromQuery] string description)
    {
        var operation = new GetByParameterExpenseCategoryQuery(categoryName,description) ;

        var result = await _mediator.Send(operation);
        return result;
    }
}