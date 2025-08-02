using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class Material
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public string FileUrl { get; set; } = string.Empty;
        
        public string FileName { get; set; } = string.Empty;
        
        public long FileSize { get; set; }
        
        public string FileType { get; set; } = string.Empty;
        
        public MaterialType Type { get; set; } = MaterialType.Document;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign keys
        public int LessonId { get; set; }
        
        // Navigation properties
        public virtual Lesson Lesson { get; set; } = null!;
    }
    
    public enum MaterialType
    {
        Document,
        Video,
        Audio,
        Image,
        Presentation,
        Other
    }
} 