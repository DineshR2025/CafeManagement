using System;
using System.Collections.Generic;
using CafeManagement.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Insfrastructure.Data;

public partial class CafeManagementContext : DbContext
{
    public CafeManagementContext()
    {
    }

    public CafeManagementContext(DbContextOptions<CafeManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cafe> Cafes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cafe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cafe__3213E83F0E133976");

            entity.ToTable("Cafe");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Location)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Logo).HasColumnName("logo");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3213E83F8BAFB737");

            entity.ToTable("Employee", tb => tb.HasTrigger("trg_GenerateEmployeeID"));

            entity.HasIndex(e => e.EmailAddress, "UQ__Employee__20C6DFF5692EC3A7").IsUnique();

            entity.HasIndex(e => e.Id, "UQ__Employee__3213E83EAFAFE2C4").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "UQ__Employee__A1936A6B947F5BE7").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.CafeId).HasColumnName("cafe_id");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email_address");
            entity.Property(e => e.Gender)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("phone_number");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Cafe).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CafeId)
                .HasConstraintName("FK__Employee__cafe_i__0B91BA14");
        });
        modelBuilder.HasSequence("EmployeeSequence").StartsAt(1000001L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
