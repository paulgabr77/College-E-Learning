using e_learning_back.DataOps.Interfaces;
using e_learning_back.Models;
using e_learning_back.DTOs;

namespace e_learning_back.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(MapToDto);
        }

        public async Task<CourseDto?> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return course != null ? MapToDto(course) : null;
        }

        public async Task<CourseDto> CreateCourseAsync(CreateCourseDto createCourseDto)
        {
            if (await _courseRepository.CodeExistsAsync(createCourseDto.Code))
            {
                throw new InvalidOperationException("Codul cursului există deja în sistem.");
            }

            var professor = await _userRepository.GetByIdAsync(createCourseDto.ProfessorId);
            if (professor == null || professor.Role != UserRole.Professor)
            {
                throw new InvalidOperationException("Profesorul nu există sau nu are rolul de profesor.");
            }

            var course = new Course
            {
                Title = createCourseDto.Title,
                Description = createCourseDto.Description,
                Code = createCourseDto.Code,
                Credits = createCourseDto.Credits,
                ProfessorId = createCourseDto.ProfessorId,
                Status = CourseStatus.Draft,
                CreatedAt = DateTime.UtcNow
            };

            var createdCourse = await _courseRepository.CreateAsync(course);
            return MapToDto(createdCourse);
        }

        public async Task<CourseDto?> UpdateCourseAsync(int id, UpdateCourseDto updateCourseDto)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            if (!string.IsNullOrEmpty(updateCourseDto.Title))
                course.Title = updateCourseDto.Title;

            if (!string.IsNullOrEmpty(updateCourseDto.Description))
                course.Description = updateCourseDto.Description;

            if (!string.IsNullOrEmpty(updateCourseDto.Code))
            {
                if (await _courseRepository.CodeExistsAsync(updateCourseDto.Code) &&
                    course.Code != updateCourseDto.Code)
                {
                    throw new InvalidOperationException("Codul cursului există deja în sistem.");
                }
                course.Code = updateCourseDto.Code;
            }

            if (updateCourseDto.Credits.HasValue)
                course.Credits = updateCourseDto.Credits.Value;

            if (updateCourseDto.Status.HasValue)
                course.Status = updateCourseDto.Status.Value;

            course.UpdatedAt = DateTime.UtcNow;

            var updatedCourse = await _courseRepository.UpdateAsync(course);
            return MapToDto(updatedCourse);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            return await _courseRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByProfessorAsync(int professorId)
        {
            var courses = await _courseRepository.GetByProfessorAsync(professorId);
            return courses.Select(MapToDto);
        }

        public async Task<IEnumerable<CourseDto>> GetCoursesByStudentAsync(int studentId)
        {
            var courses = await _courseRepository.GetEnrolledCoursesAsync(studentId);
            return courses.Select(MapToDto);
        }

        public async Task<bool> EnrollStudentAsync(int courseId, int studentId)
        {
            var course = await _courseRepository.GetByIdAsync(courseId);
            if (course == null) return false;

            var student = await _userRepository.GetByIdAsync(studentId);
            if (student == null || student.Role != UserRole.Student) return false;

            return await _courseRepository.EnrollStudentAsync(courseId, studentId);
        }

        public async Task<bool> UnenrollStudentAsync(int courseId, int studentId)
        {
            return await _courseRepository.UnenrollStudentAsync(courseId, studentId);
        }

        private static CourseDto MapToDto(Course course)
        {
            return new CourseDto
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                Code = course.Code,
                Credits = course.Credits,
                Status = course.Status,
                CreatedAt = course.CreatedAt,
                UpdatedAt = course.UpdatedAt,
                ProfessorId = course.ProfessorId,
                ProfessorName = course.Professor != null ? ($"{course.Professor.FirstName} {course.Professor.LastName}") : "Necunoscut",
                EnrolledStudentsCount = course.EnrolledStudents?.Count ?? 0,
                LessonsCount = course.Lessons?.Count ?? 0,
                AssessmentsCount = course.Assessments?.Count ?? 0
            };
        }
    }
}
