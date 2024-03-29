using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBase.Entity;

namespace Web.Data.Entity;
[Table("ExpenseCategory", Schema = "dbo")]

public class ExpenseCategory:BaseEntity
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    
    public List<Expense> Expenses { get; set; }
}
public class ExpenseCategoryConfiguration : IEntityTypeConfiguration<ExpenseCategory>
{
    public void Configure(EntityTypeBuilder<ExpenseCategory> builder)
    {
      
        builder.HasKey(ec => ec.CategoryId);

        builder.Property(ec => ec.CategoryName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(ec => ec.Description)
            .HasMaxLength(255);

        builder.HasMany(ec => ec.Expenses)
            .WithOne(e => e.Category)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict); 
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

    }
}