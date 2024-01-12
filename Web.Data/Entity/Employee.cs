using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBase.Entity;

namespace Web.Data.Entity;
[Table("Employee", Schema = "dbo")]
public class Employee:BaseEntity
{
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
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
        builder.Property(e => e.FirstName).HasMaxLength(50).IsRequired();
        builder.Property(e => e.LastName).HasMaxLength(50).IsRequired();
        builder.Property(e => e.EmployeeNumber).HasMaxLength(50).IsRequired();
        builder.Property(e => e.DateOfBirth).IsRequired();
        builder.Property(e => e.IBAN).HasMaxLength(50).IsRequired();
        builder.Property(e => e.LastActivityDate).IsRequired();

        // Configure the relationship with Expense entity
        builder.HasMany(e => e.Expenses)
            .WithOne(expense => expense.Employee)
            .HasForeignKey(expense => expense.EmployeeId);

    }
}