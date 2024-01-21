using MediatR;
using WebBase.Enum;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;

public record GelMyReportQuery(int Id):IRequest <ApiResponse<List<ReportResponse>>>;

public record GetCompanyReportQueryByPeriod(ReportTimePeriod ReportTimePeriod):IRequest<ApiResponse<List<ReportResponse>>>;

public record GetEmployeeReportQuery(int EmployeeId,ReportTimePeriod ReportTimePeriod):IRequest <ApiResponse<List<ReportResponse>>>;


public record GetCompanyReportQueryByStatusAndPeriod(ReportTimePeriod ReportTimePeriod,ExpenseStatus ExpenseStatus):IRequest <ApiResponse<List<ReportResponse>>>;

public record GetCompanyReportQueryByStatus(ExpenseStatus ExpenseStatus):IRequest <ApiResponse<List<ReportResponse>>>;

public record GetAllPaymentsReportQuery():IRequest <ApiResponse<List<PaymentResponse>>>;



