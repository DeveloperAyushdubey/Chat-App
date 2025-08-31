using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace projectApi.Models;

public partial class ChatapiContext : DbContext
{
    public ChatapiContext()
    {
    }

    public ChatapiContext(DbContextOptions<ChatapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=chatapi;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.Departmenet).HasColumnName("departmenet");
            entity.Property(e => e.Role).HasColumnName("role");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.Image1).HasColumnName("image");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.ToTable("logins");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Password).HasColumnName("password");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("messages");

          //  entity.Property(e => e.Usernumber).HasColumnName("usernumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
