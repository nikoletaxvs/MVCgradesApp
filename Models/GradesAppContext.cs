using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GradesApp.Models;

public partial class GradesAppContext : DbContext
{
    public GradesAppContext()
    {
    }

    public GradesAppContext(DbContextOptions<GradesAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseHasStudent> CourseHasStudents { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Secretary> Secretaries { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-0DOJL0DB;Database=GradesApp;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.IdCourse).HasName("PK__course__C1857757B0B5AF10");

            entity.ToTable("course");

            entity.Property(e => e.IdCourse)
                .ValueGeneratedNever()
                .HasColumnName("idCOURSE");
            entity.Property(e => e.CourseSemester)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ProfessorsAfm).HasColumnName("PROFESSORS_AFM");

            entity.HasOne(d => d.ProfessorsAfmNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ProfessorsAfm)
                .HasConstraintName("FK__course__PROFESSO__2F10007B");
        });

        modelBuilder.Entity<CourseHasStudent>(entity =>
        {
            entity.HasKey(e => e.IdCourseHasStudents).HasName("PK__course_h__D6FC01456A821A6D");

            entity.ToTable("course_has_students");

            entity.Property(e => e.IdCourseHasStudents)
                .ValueGeneratedNever()
                .HasColumnName("id_course_has_students");
            entity.Property(e => e.CourseIdCourse).HasColumnName("COURSE_idCOURSE");
            entity.Property(e => e.StudentsRegistrationNumber).HasColumnName("STUDENTS_RegistrationNumber");

            entity.HasOne(d => d.CourseIdCourseNavigation).WithMany(p => p.CourseHasStudents)
                .HasForeignKey(d => d.CourseIdCourse)
                .HasConstraintName("FK__course_ha__COURS__31EC6D26");

            entity.HasOne(d => d.StudentsRegistrationNumberNavigation).WithMany(p => p.CourseHasStudents)
                .HasForeignKey(d => d.StudentsRegistrationNumber)
                .HasConstraintName("FK__course_ha__STUDE__32E0915F");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.Afm).HasName("PK__professo__C6906E631D0346E8");

            entity.ToTable("professors");

            entity.Property(e => e.Afm)
                .ValueGeneratedNever()
                .HasColumnName("AFM");
            entity.Property(e => e.Department)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsersUsername)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("USERS_username");

            entity.HasOne(d => d.UsersUsernameNavigation).WithMany(p => p.Professors)
                .HasForeignKey(d => d.UsersUsername)
                .HasConstraintName("FK__professor__USERS__29572725");
        });

        modelBuilder.Entity<Secretary>(entity =>
        {
            entity.HasKey(e => e.Phonenumber).HasName("PK__secretar__9FDCA5A65E519C99");

            entity.ToTable("secretaries");

            entity.Property(e => e.Phonenumber).ValueGeneratedNever();
            entity.Property(e => e.Department)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsersUsername)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("USERS_username");

            entity.HasOne(d => d.UsersUsernameNavigation).WithMany(p => p.Secretaries)
                .HasForeignKey(d => d.UsersUsername)
                .HasConstraintName("FK__secretari__USERS__2C3393D0");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.RegistrationNumber).HasName("PK__students__E886460315F6F138");

            entity.ToTable("students");

            entity.Property(e => e.RegistrationNumber).ValueGeneratedNever();
            entity.Property(e => e.Department)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsersUsername)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("USERS_username");

            entity.HasOne(d => d.UsersUsernameNavigation).WithMany(p => p.Students)
                .HasForeignKey(d => d.UsersUsername)
                .HasConstraintName("FK__students__USERS___267ABA7A");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__users__F3DBC573E8223E19");

            entity.ToTable("users");

            entity.Property(e => e.Username)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
