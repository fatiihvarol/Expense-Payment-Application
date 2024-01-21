using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Enum;

namespace Web.Business.Services;

public class HavalePayment:IPayment
{
    private readonly VbDbContext _dbContext;
    
    public HavalePayment(VbDbContext dbContext)
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
            PaymentDate = DateTime.Now,
            Description = description,
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