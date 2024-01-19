using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Command.ExpenseCommand;

public class ExpenseCommandHandler :
    IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
    IRequestHandler<UpdateExpenseCommand, ApiResponse>,
    IRequestHandler<DeleteExpenseCommand, ApiResponse>

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
        entity.Status = "pending";


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

        fromDb.Status = request.Status;


        if (request.Status.Equals("approved"))
        {
            //TODO 1 make implemtation here
            //make transection
        }

        else if (request.Status.Equals("declined", StringComparison.OrdinalIgnoreCase) &&
                 request.RejectionDescription == null)
        {
            return new ApiResponse("when you declined expense you have to give rejection description");
        }


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
}