using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class Course
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;
        
        public int Credits { get; set; }
        
        public CourseStatus Status { get; set; } = CourseStatus.Draft;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign keys
        public int ProfessorId { get; set; }
        
        // Navigation properties
        public virtual User Professor { get; set; } = null!;
        public virtual ICollection<User> EnrolledStudents { get; set; } = new List<User>();
        public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public virtual ICollection<Assessment> Assessments { get; set; } = new List<Assessment>();
    }
    
    public enum CourseStatus
    {
        Draft,
        Published,
        Archived
    }
} 