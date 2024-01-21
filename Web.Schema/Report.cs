using System.Text.Json.Serialization;

namespace WebSchema;

public class ReportResponse
{
    public int ExpenseId { get; set; }
    public int EmployeeId { get; set; }
    public int CategoryId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string? RejectionDescription { get; set; }
    public string PaymentRequestType { get; set; }

    public string Document { get; set; }

    public ExpenseCategoryResponse Category { get; set; }
    public AddressResponse Address { get; set; }
    [JsonIgnore]
    public EmployeeResponse Employee { get; set; }
    public PaymentResponse Payment { get; set; }
}