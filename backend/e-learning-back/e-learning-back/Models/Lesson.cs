using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class Lesson
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(2000)]
        public string Content { get; set; } = string.Empty;
        
        public int OrderIndex { get; set; }
        
        public LessonType Type { get; set; } = LessonType.Text;
        
        public string? VideoUrl { get; set; }
        
        public string? FileUrl { get; set; }
        
        public int DurationMinutes { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public DateTime? UpdatedAt { get; set; }
        
        // Foreign keys
        public int CourseId { get; set; }
        
        // Navigation properties
        public virtual Course Course { get; set; } = null!;
        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
    }
    
    public enum LessonType
    {
        Text,
        Video,
        Document,
        Quiz,
        Assignment
    }
} 