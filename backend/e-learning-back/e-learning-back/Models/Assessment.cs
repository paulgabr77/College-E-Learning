using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class Assessment
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        public AssessmentType Type { get; set; } = AssessmentType.Quiz;
        
        public int TotalPoints { get; set; }
        
        public int DurationMinutes { get; set; }
        
        public DateTime? StartDate { get; set; }
        
        public DateTime? EndDate { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign keys
        public int CourseId { get; set; }
        
        // Navigation properties
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
        public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
    }
    
    public enum AssessmentType
    {
        Quiz,
        Assignment,
        Exam,
        Project
    }
} 