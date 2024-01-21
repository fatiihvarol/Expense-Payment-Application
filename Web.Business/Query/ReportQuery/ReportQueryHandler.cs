using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Query.ReportQuery;

public class ReportQueryHandler:
    IRequestHandler<GelMyReportQuery, ApiResponse<List<ReportResponse>>>,
    IRequestHandler<GetCompanyReportQuery, ApiResponse<List<ExpenseResponse>>>,

    IRequestHandler<GetEmployeeReportQuery, ApiResponse<List<ExpenseResponse>>>,

    IRequestHandler<GetCompanyExpensesByStatusReportQuery, ApiResponse<List<ExpenseResponse>>>


{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ReportQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ReportResponse>>> Handle(GelMyReportQuery request, CancellationToken cancellationToken)
    {
        var report = await _dbContext.Set<Expense>()
            .Include(x => x.Address)
            .Include(x => x.Category)
            .Include(x => x.Payment)
            .Include(x => x.Employee)
            .Where(x => x.EmployeeId == request.Id)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<Expense>, List<ReportResponse>>(report);

        return new ApiResponse<List<ReportResponse>>(mapped);
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetCompanyReportQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetEmployeeReportQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetCompanyExpensesByStatusReportQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}