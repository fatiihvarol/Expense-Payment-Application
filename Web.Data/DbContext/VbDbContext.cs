using Microsoft.EntityFrameworkCore;
using Web.Data.Entity;

namespace Web.Data.DbContext;

public class VbDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public VbDbContext(DbContextOptions<VbDbContext> options): base(options)
    {
    
    }   
    
    // dbset 
  
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    
    public DbSet<Employee> Employees { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Expense> Expenses { get; set; }

    public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

    public DbSet<Payment> Payments { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new ExpenseCategoryConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
}