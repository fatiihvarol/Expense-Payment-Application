using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Enum;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Command.ExpenseCommand;

public class ExpenseCommandHandler :
    IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
    IRequestHandler<UpdateExpenseCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseCommand, ApiResponse>,
    IRequestHandler<DeclineExpenseCommand, ApiResponse>

{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ExpenseCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request,
        CancellationToken cancellationToken)
    {
        var checkEmployeeId = await _dbContext.Set<Employee>()
            .Where(x => x.Id == request.Model.EmployeeId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkEmployeeId == null)
            return new ApiResponse<ExpenseResponse>("employee with this  id not found");


        var entity = _mapper.Map<ExpenseRequest, Expense>(request.Model);

        entity.InsertDate = DateTime.Now;
        entity.Status = ExpenseStatus.Pending;


        var entityResult = await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var mapped = _mapper.Map<Expense, ExpenseResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<Expense>()
            .Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromDb == null)
            return new ApiResponse("expense with this id not found");

        fromDb.Description = request.Description;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<Expense>()
            .Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromDb == null)
            return new ApiResponse("expense with this id not found");

        fromDb.IsActive = false;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeclineExpenseCommand request, CancellationToken cancellationToken)
    {
        var expense = await _dbContext.Set<Expense>()
            .FirstOrDefaultAsync(x => x.ExpenseId == request.Id, cancellationToken);
        if (expense is null)
            return new ApiResponse("expense not found with this id");

        if (expense.Status == ExpenseStatus.Approved)
            return new ApiResponse("expense already paid");

        expense.Status = ExpenseStatus.Rejected;
        expense.RejectionDescription = request.RejectionDescription;
        expense.UpdateDate = DateTime.Now;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}