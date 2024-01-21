using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBase.Entity;

namespace Web.Data.Entity;

[Table("Payment", Schema = "dbo")]
public class Payment : BaseEntity
{
    public int PaymentId { get; set; }
    public int ExpenseId { get; set; }

    public string ReceiverIban { get; set; }

    public string Description { get; set; }

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }

    public Expense Expense { get; set; }
}

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payment", "dbo");

        builder.HasKey(p => p.PaymentId);

        builder.HasOne(p => p.Expense)
            .WithOne(e => e.Payment)
            .HasForeignKey<Payment>(p => p.ExpenseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(p => p.Amount)
            .IsRequired();

        builder.Property(p => p.PaymentDate)
            .IsRequired();
        builder.Property(p => p.ReceiverIban)
            .IsRequired();
        builder.Property(p => p.Description)
            .IsRequired();
        
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        
    }
}