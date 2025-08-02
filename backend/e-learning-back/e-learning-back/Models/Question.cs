using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class Question
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Text { get; set; } = string.Empty;
        
        public QuestionType Type { get; set; } = QuestionType.MultipleChoice;
        
        public int Points { get; set; } = 1;
        
        public int OrderIndex { get; set; }
        
        public string? CorrectAnswer { get; set; }
        
        public string? Options { get; set; } // JSON array for multiple choice options
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign keys
        public int AssessmentId { get; set; }
        
        // Navigation properties
        public virtual Assessment Assessment { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
    
    public enum QuestionType
    {
        MultipleChoice,
        TrueFalse,
        ShortAnswer,
        Essay,
        FileUpload
    }
} 