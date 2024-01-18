using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Command.EmployeeCommand;

public class EmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse<EmployeeResponse>>,
    IRequestHandler<UpdateEmployeeCommand, ApiResponse>,
    IRequestHandler<DeleteEmployeeCommand, ApiResponse>

{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmployeeCommandHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<EmployeeResponse>> Handle(CreateEmployeeCommand request,
        CancellationToken cancellationToken)
    {
        var checkIdentity = await _dbContext.Set<Employee>()
            .Where(x => x.IdentityNumber == request.Model.IdentityNumber)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
            return new ApiResponse<EmployeeResponse>($"{request.Model.IdentityNumber} is used by another customer.");


        var entity = _mapper.Map<EmployeeRequest, Employee>(request.Model);

        entity.EmployeeNumber = new Random().Next(1000000, 9999999).ToString();
        entity.InsertDate = DateTime.Now;

        var entityResult = await _dbContext.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var mapped = _mapper.Map<Employee, EmployeeResponse>(entityResult.Entity);
        return new ApiResponse<EmployeeResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<Employee>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (fromDb is null)
            return new ApiResponse("employee not found to update");

        fromDb.FirstName = request.Model.FirstName;
        fromDb.LastName = request.Model.LastName;
        fromDb.IBAN = request.Model.IBAN;

        fromDb.UpdateDate = DateTime.Now;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }


    public async Task<ApiResponse> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<Employee>()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (fromDb is null)
            return new ApiResponse("employee not found to delete");

        fromDb.IsActive = false;
        fromDb.UpdateDate=DateTime.Now;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}