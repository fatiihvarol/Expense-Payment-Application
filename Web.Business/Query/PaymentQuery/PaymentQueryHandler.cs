using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Response;
using WebSchema;

namespace Web.Business.Query.PaymentQuery;

public class PaymentQueryHandler : IRequestHandler<GelAllPaymentsQuery, ApiResponse<List<PaymentResponse>>>,
    IRequestHandler<GetByIdPaymentQuery, ApiResponse<PaymentResponse>>,
    IRequestHandler<GetByParameterPaymentsQuery, ApiResponse<List<PaymentResponse>>>

{
    private readonly VbDbContext _dbContext;
    private readonly IMapper _mapper;

    public PaymentQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GelAllPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        var payments = await _dbContext.Set<Payment>()
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<Payment>, List<PaymentResponse>>(payments);

        return new ApiResponse<List<PaymentResponse>>(mapped);
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(GetByIdPaymentQuery request,
        CancellationToken cancellationToken)
    {
        var payment = await _dbContext.Set<Payment>()
            .FirstOrDefaultAsync(x => x.PaymentId == request.Id, cancellationToken);

        if (payment is null)
            return new ApiResponse<PaymentResponse>("payment with id not found");


        var mapped = _mapper.Map<Payment, PaymentResponse>(payment);

        return new ApiResponse<PaymentResponse>(mapped);
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetByParameterPaymentsQuery request,
        CancellationToken cancellationToken)
    {
        Expression<Func<Payment, bool>> filter = u =>
            (u.ExpenseId == request.ExpenseId) &&
            (u.Amount == request.Amount);

        var payments = await _dbContext.Set<Payment>()
            .Where(filter)
            .ToListAsync(cancellationToken);

        var mapped = _mapper.Map<List<Payment>, List<PaymentResponse>>(payments);

        return new ApiResponse<List<PaymentResponse>>(mapped);
    }
}