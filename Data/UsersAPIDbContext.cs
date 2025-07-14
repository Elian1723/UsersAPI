using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UsersAPI.Models;

namespace UsersAPI.Data;

public partial class UsersAPIDbContext : DbContext
{
    public UsersAPIDbContext()
    {
    }

    public UsersAPIDbContext(DbContextOptions<UsersAPIDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.DateOfBirth).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
