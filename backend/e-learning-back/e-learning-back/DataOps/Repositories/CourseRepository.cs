using Microsoft.EntityFrameworkCore;
using e_learning_back.Data;
using e_learning_back.Models;
using e_learning_back.DataOps.Interfaces;

namespace e_learning_back.DataOps.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.EnrolledStudents)
                .Include(c => c.Lessons)
                .Include(c => c.Assessments)
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.EnrolledStudents)
                .Include(c => c.Lessons)
                .Include(c => c.Assessments)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Course?> GetByCodeAsync(string code)
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.EnrolledStudents)
                .FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<Course> CreateAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> UpdateAsync(Course course)
        {
            course.UpdatedAt = DateTime.UtcNow;
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
            return course;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null) return false;

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CodeExistsAsync(string code)
        {
            return await _context.Courses.AnyAsync(c => c.Code == code);
        }

        public async Task<IEnumerable<Course>> GetByProfessorAsync(int professorId)
        {
            return await _context.Courses
                .Include(c => c.EnrolledStudents)
                .Include(c => c.Lessons)
                .Include(c => c.Assessments)
                .Where(c => c.ProfessorId == professorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetEnrolledCoursesAsync(int studentId)
        {
            return await _context.Courses
                .Include(c => c.Professor)
                .Include(c => c.Lessons)
                .Include(c => c.Assessments)
                .Where(c => c.EnrolledStudents.Any(s => s.Id == studentId))
                .ToListAsync();
        }

        public async Task<bool> EnrollStudentAsync(int courseId, int studentId)
        {
            var course = await _context.Courses
                .Include(c => c.EnrolledStudents)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            var student = await _context.Users.FindAsync(studentId);

            if (course == null || student == null) return false;

            if (!course.EnrolledStudents.Any(s => s.Id == studentId))
            {
                course.EnrolledStudents.Add(student);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<bool> UnenrollStudentAsync(int courseId, int studentId)
        {
            var course = await _context.Courses
                .Include(c => c.EnrolledStudents)
                .FirstOrDefaultAsync(c => c.Id == courseId);

            if (course == null) return false;

            var student = course.EnrolledStudents.FirstOrDefault(s => s.Id == studentId);
            if (student != null)
            {
                course.EnrolledStudents.Remove(student);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<int> GetEnrolledStudentsCountAsync(int courseId)
        {
            return await _context.Courses
                .Where(c => c.Id == courseId)
                .SelectMany(c => c.EnrolledStudents)
                .CountAsync();
        }
    }
} 