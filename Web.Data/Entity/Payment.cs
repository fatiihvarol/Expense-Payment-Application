using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        // Table mapping
        builder.ToTable("Payment", "dbo");

        // Primary key
        builder.HasKey(p => p.PaymentId);

        // Foreign key relationship
        builder.HasOne(p => p.Expense)
            .WithOne(e => e.Payment) 
            .HasForeignKey<Payment>(p => p.ExpenseId)
            .OnDelete(DeleteBehavior.Cascade); 

        // Other property configurations
        builder.Property(p => p.Amount)
            .IsRequired();

        builder.Property(p => p.PaymentDate)
            .IsRequired();

        // Note: Adjust the configurations based on your specific requirements.
    }
}