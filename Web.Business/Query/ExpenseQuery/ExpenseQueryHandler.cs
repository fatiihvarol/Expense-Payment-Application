using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Query.ExpenseQuery;

public class ExpenseQueryHandler :
    IRequestHandler<GelAllExpensesQuery, ApiResponse<List<ExpenseResponse>>>,
    IRequestHandler<GetByIdExpenseQuery, ApiResponse<ExpenseResponse>>,
    IRequestHandler<GetByParameterExpenseQuery, ApiResponse<List<ExpenseResponse>>>

{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExpenseQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GelAllExpensesQuery request,
        CancellationToken cancellationToken)
    {
        var expenses = await _dbContext.Set<Expense>()
            .Include(x => x.Address)
            .Include(x => x.Category)
            .Include(x => x.Payment)
            .Include(x => x.Employee)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<Expense>, List<ExpenseResponse>>(expenses);
        return new ApiResponse<List<ExpenseResponse>>(mapped);


    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(GetByIdExpenseQuery request,
        CancellationToken cancellationToken)
    {
        var expense = await _dbContext.Set<Expense>()
            .Include(x => x.Address)
            .Include(x => x.Category)
            .Include(x => x.Payment)
            .Include(x => x.Employee)
            .Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (expense is null)
            return new ApiResponse<ExpenseResponse>("expense not found ");

        var mapped = _mapper.Map<Expense, ExpenseResponse>(expense);

        return new ApiResponse<ExpenseResponse>(mapped);

    }

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetByParameterExpenseQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Expense, bool>> filter = u =>
            (u.CategoryId==request.CategoryId)&&
            (u.EmployeeId == request.EmployeeId) &&
            ( u.Status.ToLower() == request.Status.ToLower()) &&
            (u.Amount == request.Amount);

        var expense = await _dbContext.Set<Expense>()
            .Include(x => x.Address)
            .Include(x => x.Category)
            .Include(x => x.Payment)
            .Include(x => x.Employee)
            .Where(filter)
            .ToListAsync(cancellationToken);

        if (!expense.Any())
            return new ApiResponse<List<ExpenseResponse>>("expense not found with these filter");

        var mapped = _mapper.Map<List<Expense>, List<ExpenseResponse>>(expense);

        return new ApiResponse<List<ExpenseResponse>>(mapped);


    }
}