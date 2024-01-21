using Web.Data.Entity;

namespace Web.Business.Services;

public interface IPayment
{
    Payment MoneyTransfer(Expense  expense,string description);
}