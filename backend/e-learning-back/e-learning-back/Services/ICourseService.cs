using e_learning_back.DTOs;

namespace e_learning_back.Services
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(int id);
        Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto);
        Task<CourseDto?> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto);
        Task<bool> DeleteCourseAsync(int id);
        Task<IEnumerable<CourseDto>> GetCoursesByProfessorAsync(int professorId);
        Task<IEnumerable<CourseDto>> GetCoursesByStudentAsync(int studentId);
        Task<bool> EnrollStudentAsync(int courseId, int studentId);
        Task<bool> UnenrollStudentAsync(int courseId, int studentId);
    }
}
