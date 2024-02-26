﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoSeUsaNet7.Areas.Identity.Data;

namespace TodoSeUsaNet7.Data;

public class TodoSeUsaNet7Context : IdentityDbContext<TodoSeUsaNet7User>
{
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
    }
}
