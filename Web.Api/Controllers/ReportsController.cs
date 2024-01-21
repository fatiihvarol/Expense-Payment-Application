using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using Web.Data.Entity;
using WebBase.Enum;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMemoryCache _memoryCache;

    public ReportsController(IMediator mediator,IMemoryCache memoryCache)
    {
        _mediator = mediator;
        _memoryCache = memoryCache;
    }

   // [Authorize(Roles = "employee,admin")]
    [HttpGet("MyReport")]
    public async Task<ActionResult<ApiResponse<List<ReportResponse>>>> GetMyReport()
    {
        if (!int.TryParse(User.FindFirstValue("Id"), out var userId))
            return new ApiResponse<List<ReportResponse>>("Invalid user information");
        
        var cacheKey = $"ApplicationUserId_{userId}";
        if (_memoryCache.TryGetValue(cacheKey, out int cachedEmployeeId))
        {
            var operation = new GetMyReportQuery(cachedEmployeeId);
            var result = await _mediator.Send(operation);
            return result;
        }

        return new ApiResponse<List<ReportResponse>>("employee id not found in cache");

    }
    
    
    //[Authorize(Roles = "admin")]
    [HttpGet("ByPeriod")]
    public async Task<ActionResult<ApiResponse<List<ReportResponse>>>> GetCompanyReportByPeriod([FromQuery] ReportTimePeriod period)
    {
        var operation = new GetCompanyReportQueryByPeriod(period);
        var result = await _mediator.Send(operation);
        return result;
    }
    
    //[Authorize(Roles = "admin")]
    [HttpGet("ByEmployee")]
    public async Task<ActionResult<ApiResponse<List<ReportResponse>>>> GetCompanyReportByPeriod(int id,[FromQuery] ReportTimePeriod period)
    {
        var operation = new GetEmployeeReportQuery(id,period);
        var result = await _mediator.Send(operation);
        return result;
    }
    //[Authorize(Roles = "admin")]
    [HttpGet("ByStatus")]
    public async Task<ActionResult<ApiResponse<List<ReportResponse>>>> GetCompanyReportByStatusAndPeriod([FromQuery] ExpenseStatus status)
    {
        var operation = new GetCompanyReportQueryByStatus(status);
        var result = await _mediator.Send(operation);
        return result;
    }
   // [Authorize(Roles = "admin")]
    [HttpGet("ByStatusAndPeriod")]
    public async Task<ActionResult<ApiResponse<List<ReportResponse>>>> GetCompanyReportByStatusAndPeriod([FromQuery] ExpenseStatus status, [FromQuery] ReportTimePeriod period)
    {
        var operation = new GetCompanyReportQueryByStatusAndPeriod(period,status);
        var result = await _mediator.Send(operation);
        return result;
    }
    
    
    //[Authorize(Roles = "admin")]
    [HttpGet("Approved")]
    public async Task<ActionResult<ApiResponse<List<PaymentResponse>>>> GetCompanyReportByStatusAndPeriod()
    {
        var operation = new GetAllPaymentsReportQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    
}