using Microsoft.AspNetCore.Mvc;
using e_learning_back.Services;
using e_learning_back.DTOs;

namespace e_learning_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CoursesController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Obține toate cursurile
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCourses()
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        /// <summary>
        /// Obține un curs după ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDto>> GetCourse(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null)
                return NotFound();

            return Ok(course);
        }

        /// <summary>
        /// Creează un curs nou
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse(CreateCourseDto createCourseDto)
        {
            try
            {
                var course = await _courseService.CreateCourseAsync(createCourseDto);
                return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualizează un curs
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult<CourseDto>> UpdateCourse(int id, UpdateCourseDto updateCourseDto)
        {
            try
            {
                var course = await _courseService.UpdateCourseAsync(id, updateCourseDto);
                if (course == null)
                    return NotFound();

                return Ok(course);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Șterge un curs
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Obține cursurile unui profesor
        /// </summary>
        [HttpGet("professor/{professorId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByProfessor(int professorId)
        {
            var courses = await _courseService.GetCoursesByProfessorAsync(professorId);
            return Ok(courses);
        }

        /// <summary>
        /// Obține cursurile la care este înscris un student
        /// </summary>
        [HttpGet("student/{studentId}")]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetCoursesByStudent(int studentId)
        {
            var courses = await _courseService.GetCoursesByStudentAsync(studentId);
            return Ok(courses);
        }

        /// <summary>
        /// Înscrie un student la un curs
        /// </summary>
        [HttpPost("{courseId}/enroll/{studentId}")]
        public async Task<ActionResult> EnrollStudent(int courseId, int studentId)
        {
            var result = await _courseService.EnrollStudentAsync(courseId, studentId);
            if (!result)
                return BadRequest(new { message = "Nu s-a putut înscrie studentul la curs" });

            return Ok(new { message = "Student înscris cu succes la curs" });
        }

        /// <summary>
        /// Retrage un student de la un curs
        /// </summary>
        [HttpDelete("{courseId}/enroll/{studentId}")]
        public async Task<ActionResult> UnenrollStudent(int courseId, int studentId)
        {
            var result = await _courseService.UnenrollStudentAsync(courseId, studentId);
            if (!result)
                return BadRequest(new { message = "Nu s-a putut retrage studentul de la curs" });

            return Ok(new { message = "Student retras cu succes de la curs" });
        }
    }
}
