using MediatR;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;


public record CreateEmployeeCommand(EmployeeRequest Model ):IRequest <ApiResponse<EmployeeResponse>>;
public record UpdateEmployeeCommand(int Id,EmployeeRequest Model ):IRequest <ApiResponse>;
public record DeleteEmployeeCommand(int Id):IRequest <ApiResponse>;


public record GelAllEmployeesQuery():IRequest <ApiResponse<List<EmployeeResponse>>>;
public record GetByIdEmployeeQuery(int Id):IRequest <ApiResponse<EmployeeResponse>>;
public record GetByParameterEmployeeQuery(string IdentityNumber,string FirstName,string Lastname ):IRequest <ApiResponse<List<EmployeeResponse>>>;

