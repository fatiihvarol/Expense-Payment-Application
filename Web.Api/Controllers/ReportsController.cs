using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Business.Cqrs;
using WebBase.Response;
using WebSchema;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReportsController
{
    private readonly IMediator _mediator;

    public ReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ApiResponse<List<ReportResponse>>> GelMyReportQuery(int id)
    {
        var operation = new GelMyReportQuery(id);
        var result = await _mediator.Send(operation);
        return result;
    }
}