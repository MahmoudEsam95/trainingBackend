using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewProject.Hubs;

//namespace NewProject.Models;
namespace NewProject.Controllers;

public partial class EduacationContext : DbContext
{
    public EduacationContext()
    {
    }

    public EduacationContext(DbContextOptions<EduacationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<ClassSubject> ClassSubjects { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<GradeSubject> GradeSubjects { get; set; }

    public virtual DbSet<Stage> Stages { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCountByClass> StudentCountByClasses { get; set; }

    public virtual DbSet<StudentCountByStage> StudentCountByStages { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<TeacherWork> TeacherWorks { get; set; }

    public virtual DbSet<User> Users { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LEROI;Initial Catalog=Eduacation;User ID=test;password=123456;;Integrated Security=True;TrustServerCertificate=True;\n");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    
    modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC27837CBB60");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.GradeId).HasColumnName("GradeID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            //    entity.HasOne(d => d.Grade).WithMany(p => p.Classes)
            //        .HasForeignKey(d => d.GradeId)
            //        .HasConstraintName("FK__Classes__GradeID__3C69FB99");
            });

            modelBuilder.Entity<ClassSubject>(entity =>
            {
                entity.ToTable("Class Subject");

                entity.HasIndex(e => new { e.ClassId, e.SubjectId ,e.TeacherID }, "UC_Class_Subject_Teacher").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.ClassId).HasColumnName("ClassID");
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
                entity.Property(e => e.TeacherID).HasColumnName("TeacherID");

            });

            modelBuilder.Entity<Degree>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Degrees_1");

                entity.HasIndex(e => new { e.SubjectId, e.StudentId }, "UC_Subject_Student").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.Degree1)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("Degree");
                entity.Property(e => e.StudentId).HasColumnName("StudentID");
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                //entity.HasOne(d => d.Student).WithMany(p => p.Degrees)
                //    .HasForeignKey(d => d.StudentId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Degrees_Student");

                //entity.HasOne(d => d.Subject).WithMany(p => p.Degrees)
                //    .HasForeignKey(d => d.SubjectId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Degrees_Subjects");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Grade__3214EC275BFDC166");

                entity.ToTable("Grade");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.StageId).HasColumnName("StageID");

                //entity.HasOne(d => d.Stage).WithMany(p => p.Grades)
                //    .HasForeignKey(d => d.StageId)
                //    .HasConstraintName("FK__Grade__StageID__398D8EEE");
            });

            modelBuilder.Entity<GradeSubject>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Grade Subjects_1");

                entity.ToTable("Grade Subjects");

                entity.HasIndex(e => new { e.SubjectId, e.GradeId }, "UC_Subject_Grade").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.GradeId).HasColumnName("GradeID");
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                //entity.HasOne(d => d.Grade).WithMany(p => p.GradeSubjects)
                //    .HasForeignKey(d => d.GradeId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Grade Subjects_Grade");

                //entity.HasOne(d => d.Subject).WithMany(p => p.GradeSubjects)
                //    .HasForeignKey(d => d.SubjectId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Grade Subjects_Subjects");
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Stage__3214EC27848078F1");

                entity.ToTable("Stage");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Student__3214EC278DCF87CD");

                entity.ToTable("Student");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ClassesId).HasColumnName("ClassesID");
                entity.Property(e => e.GradeId).HasColumnName("GradeID");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.StageId).HasColumnName("StageID");

                //entity.HasOne(d => d.Classes).WithMany(p => p.Students)
                //    .HasForeignKey(d => d.ClassesId)
                //    .HasConstraintName("FK__Student__Classes__412EB0B6");

                //entity.HasOne(d => d.Grade).WithMany(p => p.Students)
                //    .HasForeignKey(d => d.GradeId)
                //    .HasConstraintName("FK__Student__GradeID__403A8C7D");

                //entity.HasOne(d => d.Stage).WithMany(p => p.Students)
                //    .HasForeignKey(d => d.StageId)
                //    .HasConstraintName("FK__Student__StageID__3F466844");
            });

            modelBuilder.Entity<StudentCountByClass>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("StudentCountByClass");

                entity.Property(e => e.ClassName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ClassesId).HasColumnName("ClassesID");
            });

            modelBuilder.Entity<StudentCountByStage>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToView("StudentCountByStage");

                entity.Property(e => e.StageId).HasColumnName("StageID");
                entity.Property(e => e.StageName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Studying Subjects");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Employees");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.Address).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<TeacherWork>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_TeacherWork_1");

                entity.ToTable("TeacherWork");

                entity.HasIndex(e => new { e.TeacherId, e.SubjectId, e.ClassId }, "UC_Teacher_Subject_Class").IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");
                entity.Property(e => e.ClassId).HasColumnName("ClassID");
                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");
                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                //entity.HasOne(d => d.Class).WithMany(p => p.TeacherWorks)
                //    .HasForeignKey(d => d.ClassId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_TeacherWork_Classes");

                //entity.HasOne(d => d.Subject).WithMany(p => p.TeacherWorks)
                //    .HasForeignKey(d => d.SubjectId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_TeacherWork_Subjects");

                //entity.HasOne(d => d.Teacher).WithMany(p => p.TeacherWorks)
                //    .HasForeignKey(d => d.TeacherId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_TeacherWork_Teachers");
            });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users");

            entity.ToTable("Users");

            entity.Property(e => e.Id).HasColumnName("Id");

            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Username");

            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Password");
        }); 

        OnModelCreatingPartial(modelBuilder);
        }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
