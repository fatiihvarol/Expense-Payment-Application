using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Business.Services;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Command.PaymentCommand;

public class PaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>,
    IRequestHandler<UpdatePaymentCommand, ApiResponse>,
    IRequestHandler<DeletePaymentCommand, ApiResponse>
{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IPaymentFactory _paymentFactory;


    public PaymentCommandHandler(VbDbContext dbContext, IMapper mapper, IPaymentFactory paymentFactory)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _paymentFactory = paymentFactory;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var expenseFromDb = await _dbContext.Set<Expense>()
            .Where(x => x.ExpenseId == request.ExpenseId)
            .FirstOrDefaultAsync(cancellationToken);
        if (expenseFromDb == null)
            return new ApiResponse<PaymentResponse>("expense with this  id not found");


        var payment = _paymentFactory.CreatePayment(expenseFromDb);
       var response= payment.MoneyTransfer(expenseFromDb, request.Description);

        var mapped = _mapper.Map<Payment, PaymentResponse>(response);
        return new  ApiResponse<PaymentResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _dbContext.Set<Payment>()
            .FirstOrDefaultAsync(x => x.PaymentId == request.Id, cancellationToken);
        if (payment is null)
            return new ApiResponse("payment with this id not found");

        payment.UpdateDate = DateTime.Now;
        payment.Description = request.Description;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromDb = await _dbContext.Set<Payment>()
            .Where(x => x.PaymentId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromDb == null)
            return new ApiResponse("payment with this id not found");

        fromDb.IsActive = false;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}