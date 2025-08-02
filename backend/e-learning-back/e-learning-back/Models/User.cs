using System.ComponentModel.DataAnnotations;

namespace e_learning_back.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Password { get; set; } = string.Empty;
        
        public UserRole Role { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public virtual ICollection<Course> EnrolledCourses { get; set; } = new List<Course>();
        public virtual ICollection<Course> TeachingCourses { get; set; } = new List<Course>();
    }
    
    public enum UserRole
    {
        Student,
        Professor,
        Admin
    }
} 