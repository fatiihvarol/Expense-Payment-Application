using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Encryption;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Command.ApplicationUserCommand;

public class ApplicationUserCommandHandler:
    IRequestHandler<CreateApplicationUserCommand, ApiResponse<ApplicationUserResponse>>,
    IRequestHandler<UpdateApplicationUserCommand,ApiResponse>,
    IRequestHandler<DeleteApplicationUserCommand,ApiResponse>
{
    
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ApplicationUserCommandHandler(VbDbContext dbContext,IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<ApiResponse<ApplicationUserResponse>> Handle(CreateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await _dbContext.Set<ApplicationUser>().Where(x => x.UserName == request.Model.UserName)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
            return new ApiResponse<ApplicationUserResponse>($"{request.Model.UserName} is in use.");

        string hashedPassword = Md5Extension.GetHash(request.Model.Password);
        var entity = _mapper.Map<ApplicationUserRequest, ApplicationUser>(request.Model);
        entity.InsertDate=DateTime.Now;
        entity.Password = hashedPassword;
        
        var entityResult = await _dbContext.AddAsync(entity, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        var mapped = _mapper.Map<ApplicationUser, ApplicationUserResponse>(entityResult.Entity);
        return new ApiResponse<ApplicationUserResponse>(mapped);

    }

    public async  Task<ApiResponse> Handle(UpdateApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<ApplicationUser>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
            return new ApiResponse("Record not found");

        if (Md5Extension.GetHash(request.Model.Password) != fromdb.Password)
        {
            var hashedPassword = Md5Extension.GetHash(request.Model.Password);
            fromdb.Password = hashedPassword;
        }
        fromdb.FirstName = request.Model.FirstName;
        fromdb.LastName = request.Model.LastName;
        fromdb.Email = request.Model.Email;
        fromdb.Role = request.Model.Role;
        
        fromdb.UpdateDate=DateTime.Now;
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
        
    }

    public async Task<ApiResponse> Handle(DeleteApplicationUserCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<ApplicationUser>().Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
            return new ApiResponse("Record not found");

        fromdb.IsActive = false;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();   
        
    }
}