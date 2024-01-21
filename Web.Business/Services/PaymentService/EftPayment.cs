using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Enum;

namespace Web.Business.Services;

public class EftPayment : IPayment
{
    private readonly VbDbContext _dbContext;

  

    public EftPayment(VbDbContext dbContext)
    {
        _dbContext = dbContext;
     
    }

    public Payment MoneyTransfer(Expense expense,string description)
    {
        var employee = _dbContext.Set<Employee>()
            .FirstOrDefault(x => x.Id == expense.EmployeeId);
        
        
        var payment = new Payment
        {
            ExpenseId = expense.ExpenseId,
            Amount = expense.Amount,
            Description = description,
            PaymentDate = DateTime.Now,
            InsertDate = DateTime.Now,
            ReceiverIban = employee.IBAN,
            IsActive = true
        };

        _dbContext.Payments.Add(payment);
        _dbContext.SaveChanges();

        expense.Status = ExpenseStatus.Approved;
        _dbContext.SaveChanges();

        return payment;
    }
}