using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using e_learning_back.DataOps.Interfaces;
using e_learning_back.Models;

namespace e_learning_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestDataController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;

        public TestDataController(IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _courseRepository = courseRepository;
        }

        /// <summary>
        /// Populează baza de date cu utilizatori și cursuri de test. Idempotent.
        /// </summary>
        [HttpPost("seed")]
        public async Task<IActionResult> Seed()
        {
            // Creează profesor
            var existingProfessor = await _userRepository.GetByEmailAsync("profesor@uni.test");
            var professor = existingProfessor;
            if (professor == null)
            {
                professor = await _userRepository.CreateAsync(new User
                {
                    FirstName = "Ion",
                    LastName = "Ionescu",
                    Email = "profesor@uni.test",
                    Password = BCrypt.Net.BCrypt.HashPassword("Parola123!"),
                    Role = UserRole.Professor,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Creează student
            var existingStudent = await _userRepository.GetByEmailAsync("student@uni.test");
            var student = existingStudent;
            if (student == null)
            {
                student = await _userRepository.CreateAsync(new User
                {
                    FirstName = "Maria",
                    LastName = "Popescu",
                    Email = "student@uni.test",
                    Password = BCrypt.Net.BCrypt.HashPassword("Parola123!"),
                    Role = UserRole.Student,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Creează curs
            var existingCourse = await _courseRepository.GetByCodeAsync("INF101");
            var course = existingCourse;
            if (course == null)
            {
                course = await _courseRepository.CreateAsync(new Course
                {
                    Title = "Introducere în Programare",
                    Description = "Bazele programării pentru începători",
                    Code = "INF101",
                    Credits = 5,
                    ProfessorId = professor.Id,
                    Status = UserRole.Professor == professor.Role ? CourseStatus.Published : CourseStatus.Draft,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // Înscrie studentul la curs (dacă nu e înscris)
            await _courseRepository.EnrollStudentAsync(course.Id, student.Id);

            return Ok(new
            {
                message = "Datele de test au fost create/actualizate cu succes.",
                professor = new { professor.Id, professor.FirstName, professor.LastName, professor.Email },
                student = new { student.Id, student.FirstName, student.LastName, student.Email },
                course = new { course.Id, course.Title, course.Code }
            });
        }
    }
}
