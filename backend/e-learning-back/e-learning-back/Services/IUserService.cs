using e_learning_back.DTOs;

namespace e_learning_back.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int id);
        Task<LoginResponseDto?> LoginAsync(LoginDto loginDto);
        Task<IEnumerable<UserDto>> GetStudentsAsync();
        Task<IEnumerable<UserDto>> GetProfessorsAsync();
    }
} 