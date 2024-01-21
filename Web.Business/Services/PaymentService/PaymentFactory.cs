using Web.Data.DbContext;
using Web.Data.Entity;

namespace Web.Business.Services;

public class PaymentFactory : IPaymentFactory
{
    private readonly VbDbContext _dbContext;

    public PaymentFactory(VbDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IPayment CreatePayment(Expense expense)
    {
        switch (expense.PaymentRequestType)
        {
            case "EFT":
                return new EftPayment(_dbContext);
            case "HAVALE":
                return new HavalePayment(_dbContext);
            default:
                throw new NotSupportedException($"Payment type '{expense.PaymentRequestType}' not supported.");
        }
    }
}