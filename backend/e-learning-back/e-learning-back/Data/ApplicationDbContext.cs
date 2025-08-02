using Microsoft.EntityFrameworkCore;
using e_learning_back.Models;

namespace e_learning_back.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationships
            modelBuilder.Entity<Course>()
                .HasMany(c => c.EnrolledStudents)
                .WithMany(u => u.EnrolledCourses)
                .UsingEntity(j => j.ToTable("CourseEnrollments"));

            // Configure User relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.TeachingCourses)
                .WithOne(c => c.Professor)
                .HasForeignKey(c => c.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Course relationships
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Assessments)
                .WithOne(a => a.Course)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Lesson relationships
            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Materials)
                .WithOne(m => m.Lesson)
                .HasForeignKey(m => m.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Assessment relationships
            modelBuilder.Entity<Assessment>()
                .HasMany(a => a.Questions)
                .WithOne(q => q.Assessment)
                .HasForeignKey(q => q.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Assessment>()
                .HasMany(a => a.Submissions)
                .WithOne(s => s.Assessment)
                .HasForeignKey(s => s.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Submission relationships
            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Student)
                .WithMany()
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Submission>()
                .HasOne(s => s.Grader)
                .WithMany()
                .HasForeignKey(s => s.GradedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Answer relationships
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Submission)
                .WithMany(s => s.Answers)
                .HasForeignKey(a => a.SubmissionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Lesson>()
                .HasIndex(l => new { l.CourseId, l.OrderIndex })
                .IsUnique();
        }
    }
} 