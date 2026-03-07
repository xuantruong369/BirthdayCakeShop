using System;
using System.Collections.Generic;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context;

public partial class BirthdayCakeShopDbContext : DbContext
{
    public BirthdayCakeShopDbContext(DbContextOptions<BirthdayCakeShopDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CakeCategory> CakeCategories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetail> ProductDetails { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CakeCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__CakeCate__19093A0BDF1DF60A");

            entity.Property(e => e.CategoryId).HasMaxLength(10);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D80BDEB134");

            entity.Property(e => e.CustomerId).HasMaxLength(10);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.CustomerType).HasMaxLength(20);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(15);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK__Customers__Usern__398D8EEE");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFC2E3A45E");

            entity.Property(e => e.OrderId).HasMaxLength(10);
            entity.Property(e => e.CustomerId).HasMaxLength(10);
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__Orders__Customer__46E78A0C");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC073913628B");

            entity.Property(e => e.OrderId).HasMaxLength(10);
            entity.Property(e => e.ProductDetailId).HasMaxLength(10);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__49C3F6B7");

            entity.HasOne(d => d.ProductDetail).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductDetailId)
                .HasConstraintName("FK__OrderDeta__Produ__4AB81AF0");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CDCF0133DD");

            entity.Property(e => e.ProductId).HasMaxLength(10);
            entity.Property(e => e.CategoryId).HasMaxLength(10);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.MaxPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MinPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(150);
            entity.Property(e => e.Thumbnail).HasMaxLength(255);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Products__Catego__3E52440B");
        });

        modelBuilder.Entity<ProductDetail>(entity =>
        {
            entity.HasKey(e => e.ProductDetailId).HasName("PK__ProductD__3C8DD6B49230BA6C");

            entity.Property(e => e.ProductDetailId).HasMaxLength(10);
            entity.Property(e => e.CakeSize).HasMaxLength(20);
            entity.Property(e => e.Discount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Flavor).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductId).HasMaxLength(10);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductDe__Produ__412EB0B6");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProductI__7516F70C907B9D4B");

            entity.Property(e => e.ImageFile).HasMaxLength(255);
            entity.Property(e => e.ProductId).HasMaxLength(10);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductIm__Produ__440B1D61");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Users__536C85E54A93EA52");

            entity.Property(e => e.Username).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserRole).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
