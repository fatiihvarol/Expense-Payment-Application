namespace Web.Business.Cqrs;

using MediatR;
using WebBase.Response;
using WebSchema;



public record CreateExpenseCommand(ExpenseRequest Model ):IRequest <ApiResponse<ExpenseResponse>>;
public record UpdateExpenseCommand(int Id, string Description):IRequest <ApiResponse>;
public record DeleteExpenseCommand(int Id):IRequest <ApiResponse>;


public record GelAllExpensesQuery():IRequest <ApiResponse<List<ExpenseResponse>>>;
public record GetByIdExpenseQuery(int Id):IRequest <ApiResponse<ExpenseResponse>>;
public record GetByParameterExpenseQuery(int CategoryId,int EmployeeId,string Status,decimal Amount ):IRequest <ApiResponse<List<ExpenseResponse>>>;

