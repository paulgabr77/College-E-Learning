using e_learning_back.Models;
using e_learning_back.DTOs;

namespace e_learning_back.DataOps.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task<IEnumerable<User>> GetStudentsAsync();
        Task<IEnumerable<User>> GetProfessorsAsync();
    }
} 