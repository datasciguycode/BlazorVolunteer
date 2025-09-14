using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Volunteer.Models;

public partial class VolunteerContext : DbContext
{
    public VolunteerContext()
    {
    }

    public VolunteerContext(DbContextOptions<VolunteerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Interest> Interest { get; set; }

    public virtual DbSet<Role> Role { get; set; }

    public virtual DbSet<Skill> Skill { get; set; }

    public virtual DbSet<Source> Source { get; set; }

    public virtual DbSet<Status> Status { get; set; }

    public virtual DbSet<User> User { get; set; }

    public virtual DbSet<UserInterest> UserInterest { get; set; }

    public virtual DbSet<UserSkill> UserSkill { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=np:\\\\.\\pipe\\MSSQL$SQLEXPRESS\\sql\\query;Database=Volunteer;Trusted_Connection=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Interest>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Interest_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Role_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Skill_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Source>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Source_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_Status_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_User_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StatusId).HasDefaultValue(1);

            entity.HasOne(d => d.Source).WithMany(p => p.User)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Source");
        });

        modelBuilder.Entity<UserInterest>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_UserInterest_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Interest).WithMany(p => p.UserInterest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInterest_Interest");

            entity.HasOne(d => d.User).WithMany(p => p.UserInterest)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserInterest_User");
        });

        modelBuilder.Entity<UserSkill>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("TR_UserSkill_UpdateLastModified"));

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.DateModified).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Skill).WithMany(p => p.UserSkill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSkill_Skill");

            entity.HasOne(d => d.User).WithMany(p => p.UserSkill)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserSkill_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
