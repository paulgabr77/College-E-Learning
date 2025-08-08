using System.ComponentModel.DataAnnotations;
using e_learning_back.Models;

namespace e_learning_back.DTOs
{
    // DTO pentru crearea unui curs nou
    public class CreateCourseDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;
        
        [Range(1, 10)]
        public int Credits { get; set; }
        
        [Required]
        public int ProfessorId { get; set; }
    }

    // DTO pentru actualizarea unui curs
    public class UpdateCourseDto
    {
        [StringLength(200)]
        public string? Title { get; set; }
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [StringLength(50)]
        public string? Code { get; set; }
        
        [Range(1, 10)]
        public int? Credits { get; set; }
        
        public CourseStatus? Status { get; set; }
    }

    // DTO pentru răspunsul cu datele cursului
    public class CourseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; }
        public CourseStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int ProfessorId { get; set; }
        public string ProfessorName { get; set; } = string.Empty;
        public int EnrolledStudentsCount { get; set; }
        public int LessonsCount { get; set; }
        public int AssessmentsCount { get; set; }
    }

    // DTO pentru curs cu detalii complete
    public class CourseDetailDto : CourseDto
    {
        public List<UserDto> EnrolledStudents { get; set; } = new List<UserDto>();
        public List<LessonDto> Lessons { get; set; } = new List<LessonDto>();
        public List<AssessmentDto> Assessments { get; set; } = new List<AssessmentDto>();
    }

    // DTO pentru lista de cursuri
    public class CourseListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int Credits { get; set; }
        public CourseStatus Status { get; set; }
        public string ProfessorName { get; set; } = string.Empty;
        public int EnrolledStudentsCount { get; set; }
    }

    // DTO pentru înscrierea la curs
    public class EnrollCourseDto
    {
        [Required]
        public int CourseId { get; set; }
        
        [Required]
        public int StudentId { get; set; }
    }

    // DTO minim pentru Lesson (pentru a evita dependențe circulare)
    public class LessonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
    }

    // DTO minim pentru Assessment
    public class AssessmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
    }

    // Folosim enum-ul din Models
} 