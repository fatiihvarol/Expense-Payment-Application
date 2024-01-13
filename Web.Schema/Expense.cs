using System.Text.Json.Serialization;
using Web.Data.Entity;

namespace WebSchema;

public class ExpenseRequest
{
    [JsonIgnore]
    public int ExpenseId { get; set; }
    
    public int EmployeeId { get; set; }
    public int CategoryId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string? RejectionDescription { get; set; }
    public string Document { get; set; }
    
}


public class ExpenseResponse
{
    public int ExpenseId { get; set; }
    public int EmployeeId { get; set; }
    public int CategoryId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string RejectionDescription { get; set; }

    public string Document { get; set; }

    public ExpenseCategory Category { get; set; }
    public Address Address { get; set; }
    public Employee Employee { get; set; }
    public Payment Payment { get; set; }
}