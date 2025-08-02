using e_learning_back.Models;

namespace e_learning_back.DataOps.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task<Course?> GetByCodeAsync(string code);
        Task<Course> CreateAsync(Course course);
        Task<Course> UpdateAsync(Course course);
        Task<bool> DeleteAsync(int id);
        Task<bool> CodeExistsAsync(string code);
        Task<IEnumerable<Course>> GetByProfessorAsync(int professorId);
        Task<IEnumerable<Course>> GetEnrolledCoursesAsync(int studentId);
        Task<bool> EnrollStudentAsync(int courseId, int studentId);
        Task<bool> UnenrollStudentAsync(int courseId, int studentId);
        Task<int> GetEnrolledStudentsCountAsync(int courseId);
    }
} 