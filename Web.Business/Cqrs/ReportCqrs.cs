using MediatR;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;

public record GelMyReportQuery(int Id):IRequest <ApiResponse<List<ReportResponse>>>;

public record GetCompanyReportQuery(string Period):IRequest<ApiResponse<List<ExpenseResponse>>>;

public record GetEmployeeReportQuery(int EmployeeId,string Period):IRequest <ApiResponse<List<ExpenseResponse>>>;


public record GetCompanyExpensesByStatusReportQuery(string Status,string Period):IRequest <ApiResponse<List<ExpenseResponse>>>;





