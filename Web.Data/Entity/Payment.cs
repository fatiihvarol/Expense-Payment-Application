using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Entity;
[Table("Payment", Schema = "dbo")]
public class Payment
{
    public int PaymentId { get; set; }
    public int ExpenseId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public Expense Expense { get; set; }
}
