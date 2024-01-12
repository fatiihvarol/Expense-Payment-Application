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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
}