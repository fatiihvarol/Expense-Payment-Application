using MediatR;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;

public record CreateApplicationUserCommand(ApplicationUserRequest Model ):IRequest <ApiResponse<ApplicationUserResponse>>;
public record UpdateApplicationUserCommand(ApplicationUserRequest Model ):IRequest <ApiResponse<ApplicationUserResponse>>;
public record DeleteApplicationUserCommand(ApplicationUserRequest Model ):IRequest <ApiResponse<ApplicationUserResponse>>;


public record GelAllApplicationUserQuery():IRequest <ApiResponse<List<ApplicationUserResponse>>>;
public record GetByIdApplicationUserQuery(int Id):IRequest <ApiResponse<ApplicationUserResponse>>;
public record GetByParameterApplicationUserQuery(string Role,string Email,string FirstName,string Lastname ):IRequest <ApiResponse<List<ApplicationUserResponse>>>;

