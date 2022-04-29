﻿using HilbertOS.BackendApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HilbertOS.BackendApp.Database;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<NewsPost> NewsPosts { get; set; } = null!; // null! tells the compiler to shut up about NewsPosts being null

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
