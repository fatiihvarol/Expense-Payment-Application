using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebBase.Entity;

namespace Web.Data.Entity;

[Table("Address", Schema = "dbo")]
public class Address : BaseEntityWithId
{
    public int ExpenseId { get; set; }
    public virtual Expense Expense { get; set; }

    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public bool IsDefault { get; set; }
}
public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(a => a.Address1).HasMaxLength(255).IsRequired();
        builder.Property(a => a.Address2).HasMaxLength(255);
        builder.Property(a => a.Country).HasMaxLength(100).IsRequired();
        builder.Property(a => a.City).HasMaxLength(100).IsRequired();
        builder.Property(a => a.County).HasMaxLength(100);
        builder.Property(a => a.PostalCode).HasMaxLength(20);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);

      

        builder.HasIndex(a => a.Id);
    }
}