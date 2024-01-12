using System.ComponentModel.DataAnnotations.Schema;
using WebBase.Entity;

namespace Web.Data.Entity;
[Table("Expense", Schema = "dbo")]

public class Expense:BaseEntity
{
    public int ExpenseId { get; set; }
    public int EmployeeId { get; set; }
    public int CategoryId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string RejactionDescription { get; set; }

    public string Document { get; set; }

    public Employee Employee { get; set; }
    public ExpenseCategory Category { get; set; }
    public Address Address { get; set; }
    public Payment Payment { get; set; }


}