using MediatR;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Cqrs;

public record CreatePaymentCommand(int ExpenseId,string Description ):IRequest <ApiResponse<PaymentResponse>>;
public record UpdatePaymentCommand(int Id, string Description):IRequest <ApiResponse>;
public record DeletePaymentCommand(int Id):IRequest <ApiResponse>;


public record GelAllPaymentsQuery():IRequest <ApiResponse<List<PaymentResponse>>>;
public record GetByIdPaymentQuery(int Id):IRequest <ApiResponse<PaymentResponse>>;
public record GetByParameterPaymentsQuery(int ExpenseId,string ReceiverIban,decimal Amount ):IRequest <ApiResponse<List<PaymentResponse>>>;

