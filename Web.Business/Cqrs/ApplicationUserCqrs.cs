using MediatR;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;

public record CreateApplicationUserCommand(ApplicationUserRequest Model ):IRequest <ApiResponse<ApplicationUserResponse>>;
public record UpdateApplicationUserCommand(int Id,ApplicationUserRequest Model ):IRequest <ApiResponse>;
public record DeleteApplicationUserCommand(int Id):IRequest <ApiResponse>;


public record GelAllApplicationUserQuery():IRequest <ApiResponse<List<ApplicationUserResponse>>>;
public record GetByIdApplicationUserQuery(int Id):IRequest <ApiResponse<ApplicationUserResponse>>;
public record GetByParameterApplicationUserQuery(string Role,string Email,string FirstName,string Lastname ):IRequest <ApiResponse<List<ApplicationUserResponse>>>;

