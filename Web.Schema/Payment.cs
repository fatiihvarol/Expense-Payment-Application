using System.Text.Json.Serialization;

namespace WebSchema;

public class PaymentRequest
{
    [JsonIgnore]
    public int PaymentId { get; set; }
    
    public int ExpenseId { get; set; }
    public string ReceiverIban { get; set; }
    public string PaymentType { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

}


public class PaymentResponse
{
    public int PaymentId { get; set; }
    public int ExpenseId { get; set; }

    public string ReceiverIban { get; set; }
    public string PaymentType { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

}