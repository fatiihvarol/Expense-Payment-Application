using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBase.Entity;

namespace Web.Data.Entity;
[Table("Employee", Schema = "dbo")]
public class Employee:BaseEntityWithId
{
    public int ApplicationUserId { get; set; } // Foreign key for ApplicationUser
    public ApplicationUser ApplicationUser { get; set; } 
    public string IdentityNumber { get; set; } 
    
    public string EmployeeNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string IBAN { get; set; }
    public DateTime LastActivityDate { get; set; }
    
    public List<Expense> Expenses { get; set; }

    
    
    
} public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.IdentityNumber).HasMaxLength(50).IsRequired();
        builder.Property(e => e.EmployeeNumber).HasMaxLength(50).IsRequired();
        builder.Property(e => e.DateOfBirth).IsRequired();
        builder.Property(e => e.IBAN).HasMaxLength(50).IsRequired();
        builder.Property(e => e.LastActivityDate).IsRequired();
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

        // Configure the relationship with Expense entity
        builder.HasMany(e => e.Expenses)
            .WithOne(expense => expense.Employee)
            .HasForeignKey(expense => expense.EmployeeId);

        builder.HasOne(e => e.ApplicationUser)
            .WithMany()
            .HasForeignKey(e => e.ApplicationUserId)
            .IsRequired();
    }
}