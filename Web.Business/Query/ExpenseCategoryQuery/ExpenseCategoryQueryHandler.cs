using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Query.ExpenseCategoryQuery;

public class ExpenseCategoryQueryHandler:
    IRequestHandler<GelAllExpenseCategoriesQuery, ApiResponse<List<ExpenseCategoryResponse>>>,
    IRequestHandler<GetByIdExpenseCategoryQuery, ApiResponse<ExpenseCategoryResponse>>,
    IRequestHandler<GetByParameterExpenseCategoryQuery, ApiResponse<List<ExpenseCategoryResponse>>>
    
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExpenseCategoryQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GelAllExpenseCategoriesQuery request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<ExpenseCategory>()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<ExpenseCategory>,List< ExpenseCategoryResponse>>(fromDb);

        return new ApiResponse<List<ExpenseCategoryResponse>>(mapped);

    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(GetByIdExpenseCategoryQuery request,
        CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<ExpenseCategory>()
            .FirstOrDefaultAsync(x => x.CategoryId == request.Id, cancellationToken);

        if (fromDb == null)
           return new ApiResponse<ExpenseCategoryResponse>("expense category with this id not found");

        var mapped = _mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(fromDb);
        return new ApiResponse<ExpenseCategoryResponse>(mapped);


    }

    public async Task<ApiResponse<List<ExpenseCategoryResponse>>> Handle(GetByParameterExpenseCategoryQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<ExpenseCategory, bool>> filter = u =>
            (u.CategoryName.ToLower().Equals(request.CategoryName.ToLower())) &&
            (u.Description.ToLower().Equals(request.Description.ToLower()));

        var fromDb = await _dbContext.Set<ExpenseCategory>()
            .Where(filter)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<ExpenseCategory>, List<ExpenseCategoryResponse>>(fromDb);

        return new ApiResponse<List<ExpenseCategoryResponse>>(mapped);

    }
}