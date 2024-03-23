using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AspCrudApp.Models;

public partial class AspCrudAppContext : DbContext
{
    public AspCrudAppContext()
    {
    }

    public AspCrudAppContext(DbContextOptions<AspCrudAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__products__C5705938C167BD28");

            entity.ToTable("products");

            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.Pdescription).IsUnicode(false);
            entity.Property(e => e.Pname)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("PName");
            entity.Property(e => e.Pprice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D1527220");

            entity.Property(e => e.City)
                .HasMaxLength(330)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(330)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(330)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(22)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
