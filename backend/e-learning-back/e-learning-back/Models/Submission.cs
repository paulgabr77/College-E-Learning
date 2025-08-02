using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class Submission
    {
        public int Id { get; set; }
        
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;
        
        public int? Score { get; set; }
        
        public int? MaxScore { get; set; }
        
        public SubmissionStatus Status { get; set; } = SubmissionStatus.Submitted;
        
        public string? Feedback { get; set; }
        
        public DateTime? GradedAt { get; set; }
        
        public int? GradedBy { get; set; }
        
        // Foreign keys
        public int AssessmentId { get; set; }
        public int StudentId { get; set; }
        
        // Navigation properties
        public virtual Assessment Assessment { get; set; } = null!;
        public virtual User Student { get; set; } = null!;
        public virtual User? Grader { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
    
    public enum SubmissionStatus
    {
        Submitted,
        Graded,
        Late,
        Incomplete
    }
} 