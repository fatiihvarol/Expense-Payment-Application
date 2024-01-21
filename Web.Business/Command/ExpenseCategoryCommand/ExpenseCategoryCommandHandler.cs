using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Enum;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Command.ExpenseCategoryCommand;

public class ExpenseCategoryCommandHandler:
    IRequestHandler<CreateExpenseCategoryCommand,ApiResponse<ExpenseCategoryResponse>>,
    IRequestHandler<UpdateExpenseCategoryCommand,ApiResponse>,
    IRequestHandler<DeleteExpenseCategoryCommand,ApiResponse>

    

{
    
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExpenseCategoryCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseCategoryResponse>> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<ExpenseCategoryRequest, ExpenseCategory>(request.Model);

        entity.InsertDate = DateTime.Now;

        var response = await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var mapped = _mapper.Map<ExpenseCategory, ExpenseCategoryResponse>(entity);
        return new ApiResponse<ExpenseCategoryResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<ExpenseCategory>()
            .FirstOrDefaultAsync(x => x.CategoryId == request.Id, cancellationToken);
    
        if (fromDb is null)
            return new ApiResponse($"Expense category with {request.Id} id not found");

        fromDb.Description = request.Model.Description;
        fromDb.CategoryName = request.Model.CategoryName;
        
        fromDb.UpdateDate=DateTime.Now;
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }


    public async Task<ApiResponse> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<ExpenseCategory>()
            .FirstOrDefaultAsync(x => x.CategoryId == request.Id, cancellationToken);
    
        if (fromDb is null)
            return new ApiResponse($"Expense category with {request.Id} id not found");

        var expenseRequests = await _dbContext.Set<Expense>()
            .FirstOrDefaultAsync(x => x.CategoryId == request.Id, cancellationToken);
        
        if (expenseRequests is not null)
            return new ApiResponse($"Expense category with {request.Id} has payment requests you can not delete");

        fromDb.IsActive = false;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ApiResponse();
    }
}