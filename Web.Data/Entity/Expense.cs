using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBase.Entity;

namespace Web.Data.Entity;

[Table("Expense", Schema = "dbo")]
public class Expense : BaseEntity
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

public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
{
    public void Configure(EntityTypeBuilder<Expense> builder)
    {
        // Primary key
        builder.HasKey(e => e.ExpenseId);


        builder.HasOne(e => e.Category)
            .WithMany(c => c.Expenses) // Many side of the relationship
            .HasForeignKey(e => e.CategoryId);

        builder.HasOne(e => e.Address)
            .WithOne(a => a.Expense) // One side of the relationship
            .HasForeignKey<Address>(a => a.ExpenseId) // Assuming you have ExpenseId property in Address entity
            .IsRequired();

        builder.Property(e => e.Date)
            .IsRequired();

        builder.Property(e => e.Amount)
            .IsRequired();

        builder.Property(e => e.Status)
            .IsRequired();

        builder.Property(e => e.Description)
            .HasMaxLength(255);

        builder.Property(e => e.RejectionDescription)
            .HasMaxLength(255);

        builder.Property(e => e.Document)
            .HasMaxLength(255);

    }
}
