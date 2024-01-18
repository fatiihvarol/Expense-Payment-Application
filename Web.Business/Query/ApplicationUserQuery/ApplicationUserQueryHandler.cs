using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Query.ApplicationUserQuery;
public class ApplicationUserQueryHandler :
    IRequestHandler<GelAllApplicationUserQuery, ApiResponse<List<ApplicationUserResponse>>>,
    IRequestHandler<GetByIdApplicationUserQuery, ApiResponse<ApplicationUserResponse>>,
    IRequestHandler<GetByParameterApplicationUserQuery, ApiResponse<List<ApplicationUserResponse>>>
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public ApplicationUserQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<ApplicationUserResponse>>> Handle(GelAllApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _dbContext.Set<ApplicationUser>()
            .ToListAsync(cancellationToken);
        var mapped = _mapper.Map<List<ApplicationUser>, List<ApplicationUserResponse>>(users);

        return new ApiResponse<List<ApplicationUserResponse>>(mapped);
    }

    public async Task<ApiResponse<ApplicationUserResponse>> Handle(GetByIdApplicationUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Set<ApplicationUser>()
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

        if (user == null)
            return new ApiResponse<ApplicationUserResponse>("user not found");
        

        var mappedUser = _mapper.Map<ApplicationUser, ApplicationUserResponse>(user);
        return new ApiResponse<ApplicationUserResponse>(mappedUser);
    }

    public async Task<ApiResponse<List<ApplicationUserResponse>>> Handle(GetByParameterApplicationUserQuery request, CancellationToken cancellationToken)
    {
        // Check if all properties are null
        if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.Lastname) && 
            string.IsNullOrWhiteSpace(request.FirstName) && string.IsNullOrWhiteSpace(request.Role))
        {
            return new ApiResponse<List<ApplicationUserResponse>>("No parameters provided");
        }

        // Define a predicate for filtering
        Expression<Func<ApplicationUser, bool>> filter = u =>
            (string.IsNullOrWhiteSpace(request.Email) || u.Email.ToLower() == request.Email.ToLower()) &&
            (string.IsNullOrWhiteSpace(request.Lastname) || u.LastName.ToLower() == request.Lastname.ToLower()) &&
            (string.IsNullOrWhiteSpace(request.FirstName) || u.FirstName.ToLower() == request.FirstName.ToLower()) &&
            (string.IsNullOrWhiteSpace(request.Role) || u.Role.ToLower() == request.Role.ToLower());

        // Apply the filter and retrieve users
        var users = await _dbContext.Set<ApplicationUser>()
            .Where(filter)
            .ToListAsync(cancellationToken);

        // Check if any users are found
        if (!users.Any())
            return new ApiResponse<List<ApplicationUserResponse>>("No users found");

        // Map and return the result
        var mappedUsers = _mapper.Map<List<ApplicationUser>, List<ApplicationUserResponse>>(users);
        return new ApiResponse<List<ApplicationUserResponse>>(mappedUsers);
    }





}
