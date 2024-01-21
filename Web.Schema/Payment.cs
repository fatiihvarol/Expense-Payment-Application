
namespace WebSchema;

public class PaymentResponse
{
    public int PaymentId { get; set; }
    public int ExpenseId { get; set; }
    public string ReceiverIban { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

}