using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoSeUsaNet7.Models.Data;

public class TodoSeUsaNet7Context : IdentityDbContext<TodoSeUsaNet7User>
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public TodoSeUsaNet7Context(DbContextOptions<TodoSeUsaNet7Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.Entity<Client>().ToTable("Client");
        builder.Entity<Client>(entity =>
        {
            entity.Property(e => e.TotalProducts).HasDefaultValue(0);
            entity.Property(e => e.ProductsSold).HasDefaultValue(0);
            entity.Property(e => e.TotalAmountPerProducts).HasDefaultValue(0);
            entity.Property(e => e.TotalBills).HasDefaultValue(0);
            entity.Property(e => e.TotalAmountSold).HasDefaultValue(0);
        });
        
        builder.Entity<Bill>().ToTable("Bill", tb => tb.HasTrigger("UpdateClientOnBillChange"));
            builder.Entity<Bill>(entity =>
            {
                entity.Property(e => e.ProductsSold).HasDefaultValue(0);
                entity.Property(e => e.TotalAmountPerProducts).HasDefaultValue(0);
                entity.Property(e => e.TotalAmountSold).HasDefaultValue(0);
                entity.Property(e => e.TotalProducts).HasDefaultValue(0);
                entity.Property(e => e.DateCreated).HasDefaultValue(DateTime.Now);
            });        
        builder.Entity<Product>().ToTable("Product", tb => tb.HasTrigger("UpdateBillOnProductChange"));
        builder.Entity<Product>(entity =>
        {
            entity.Property(e => e.MustReturn).HasDefaultValue(false);
            entity.Property(e => e.Reaconditioned).HasDefaultValue(false);
            entity.Property(e => e.Returned).HasDefaultValue(false);
            entity.Property(e => e.ReaconditioningCost).HasDefaultValue(0);
        });
        builder.Entity<Sale>().ToTable("Sale");
        /*.ToTable(tb => tb.HasTrigger("SaleTrigger"));
        builder.Entity<Sale>(entity =>
        {
            entity.Property(e => e.Paid).HasDefaultValue(false);
            entity.Property(e => e.TotalProducts).HasDefaultValue(0);
            entity.Property(e => e.DateOfIssue).HasDefaultValue(DateTime.Now);
            entity.Property(e => e.TotalProducts).HasDefaultValue(0);
            entity.Property(e => e.Amount).HasDefaultValue(0);
            entity.Property(e => e.Paid).HasDefaultValue(0);
            entity.Property(e => e.Owes).HasDefaultValue(0);
        });*/
    }
}
