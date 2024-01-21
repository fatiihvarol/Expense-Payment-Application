using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Query.EmployeeQuery;

public class EmployeeQueryHandler : IRequestHandler<GelAllEmployeesQuery, ApiResponse<List<EmployeeResponse>>>,
    IRequestHandler<GetByIdEmployeeQuery, ApiResponse<EmployeeResponse>>,
    IRequestHandler<GetByParameterEmployeeQuery, ApiResponse<List<EmployeeResponse>>>

{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmployeeQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<EmployeeResponse>>> Handle(GelAllEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        var employees = await _dbContext.Set<Employee>()
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Category)
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Address)
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Payment)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<Employee>, List<EmployeeResponse>>(employees);
        return new ApiResponse<List<EmployeeResponse>>(mapped);
    }

    public async Task<ApiResponse<EmployeeResponse>> Handle(GetByIdEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        var employee = await _dbContext.Set<Employee>()
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Category)
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Address)
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Payment)
            .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);

        if (employee == null)
            return new ApiResponse<EmployeeResponse>("Employee not found");

        var mappedEmployee = _mapper.Map<Employee, EmployeeResponse>(employee);
        return new ApiResponse<EmployeeResponse>(mappedEmployee);
    }

    public async Task<ApiResponse<List<EmployeeResponse>>> Handle(GetByParameterEmployeeQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Employee, bool>> filter = u =>
            (string.IsNullOrWhiteSpace(request.FirstName) ||
            (string.IsNullOrWhiteSpace(request.IdentityNumber) ||
             u.IdentityNumber.ToLower() == request.IdentityNumber.ToLower()));

        var employees = await _dbContext.Set<Employee>()
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Category)
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Address)
            .Include(x => x.Expenses)
            .ThenInclude(x => x.Payment)
            .Where(filter).ToListAsync(cancellationToken);

        if (!employees.Any())
            return new ApiResponse<List<EmployeeResponse>>("No employees found");

        var mappedEmployees = _mapper.Map<List<Employee>, List<EmployeeResponse>>(employees);
        return new ApiResponse<List<EmployeeResponse>>(mappedEmployees);
    }
}