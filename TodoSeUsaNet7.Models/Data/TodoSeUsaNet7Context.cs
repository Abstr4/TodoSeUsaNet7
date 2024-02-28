﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TodoSeUsaNet7.Models.Data;

public class TodoSeUsaNet7Context : IdentityDbContext<TodoSeUsaNet7User>
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<Product> Product { get; set; }
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
        builder.Entity<Bill>().ToTable("Bill");
        builder.Entity<Product>().ToTable("Product");
        builder.Entity<Sale>().ToTable("Sale");
    }
}
