using MediatR;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;

public record CreateExpenseCategoryCommand(ExpenseCategoryRequest Model ):IRequest <ApiResponse<ExpenseCategoryResponse>>;
public record UpdateExpenseCategoryCommand(int Id,EmployeeRequest Model ):IRequest <ApiResponse>;
public record DeleteExpenseCategoryCommand(int Id):IRequest <ApiResponse>;


public record GelAllExpenseCategoriesQuery():IRequest <ApiResponse<List<ExpenseCategoryResponse>>>;
public record GetByIdExpenseCategoryQuery(int Id):IRequest <ApiResponse<ExpenseCategoryResponse>>;
public record GetByParameterExpenseCategoryQuery(string CategoryName,string Description ):IRequest <ApiResponse<List<ExpenseCategoryResponse>>>;

