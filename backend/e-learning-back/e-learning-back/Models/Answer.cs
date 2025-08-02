using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class Answer
    {
        public int Id { get; set; }
        
        [StringLength(2000)]
        public string? TextAnswer { get; set; }
        
        public string? SelectedOptions { get; set; } // JSON array for multiple choice answers
        
        public string? FileUrl { get; set; }
        
        public int? Points { get; set; }
        
        public string? Feedback { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign keys
        public int QuestionId { get; set; }
        public int SubmissionId { get; set; }
        
        // Navigation properties
        public virtual Question Question { get; set; } = null!;
        public virtual Submission Submission { get; set; } = null!;
    }
} 