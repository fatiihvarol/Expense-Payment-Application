using Web.Data.Entity;

namespace Web.Business.Services;

public interface IPaymentFactory
{
    IPayment CreatePayment(Expense expense);
}