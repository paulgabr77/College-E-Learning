using Microsoft.EntityFrameworkCore;
using e_learning_back.Data;
using e_learning_back.Models;
using e_learning_back.DataOps.Interfaces;

namespace e_learning_back.DataOps.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.TeachingCourses)
                .Include(u => u.EnrolledCourses)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.TeachingCourses)
                .Include(u => u.EnrolledCourses)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.TeachingCourses)
                .Include(u => u.EnrolledCourses)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetStudentsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Student && u.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<User>> GetProfessorsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Professor && u.IsActive)
                .ToListAsync();
        }
    }
} 