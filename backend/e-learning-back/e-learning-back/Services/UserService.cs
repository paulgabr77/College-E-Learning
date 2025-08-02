using e_learning_back.DataOps.Interfaces;
using e_learning_back.Models;
using e_learning_back.DTOs;
using e_learning_back.Services;

namespace e_learning_back.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user != null ? MapToDto(user) : null;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Verifică dacă email-ul există deja
            if (await _userRepository.EmailExistsAsync(createUserDto.Email))
            {
                throw new InvalidOperationException("Email-ul există deja în sistem.");
            }

            // Hash password-ul (în producție ar trebui să folosești BCrypt sau similar)
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password);

            var user = new User
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                Email = createUserDto.Email,
                Password = hashedPassword,
                Role = createUserDto.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            var createdUser = await _userRepository.CreateAsync(user);
            return MapToDto(createdUser);
        }

        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            // Actualizează doar câmpurile care nu sunt null
            if (!string.IsNullOrEmpty(updateUserDto.FirstName))
                user.FirstName = updateUserDto.FirstName;

            if (!string.IsNullOrEmpty(updateUserDto.LastName))
                user.LastName = updateUserDto.LastName;

            if (!string.IsNullOrEmpty(updateUserDto.Email))
            {
                // Verifică dacă noul email nu există deja
                if (await _userRepository.EmailExistsAsync(updateUserDto.Email) && 
                    user.Email != updateUserDto.Email)
                {
                    throw new InvalidOperationException("Email-ul există deja în sistem.");
                }
                user.Email = updateUserDto.Email;
            }

            if (updateUserDto.IsActive.HasValue)
                user.IsActive = updateUserDto.IsActive.Value;

            var updatedUser = await _userRepository.UpdateAsync(user);
            return MapToDto(updatedUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);
            if (user == null || !user.IsActive) return null;

            // Verifică password-ul
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
                return null;

            // În producție, aici ai genera un JWT token
            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                User = MapToDto(user),
                Token = token,
                ExpiresAt = DateTime.UtcNow.AddHours(24)
            };
        }

        public async Task<IEnumerable<UserDto>> GetStudentsAsync()
        {
            var students = await _userRepository.GetStudentsAsync();
            return students.Select(MapToDto);
        }

        public async Task<IEnumerable<UserDto>> GetProfessorsAsync()
        {
            var professors = await _userRepository.GetProfessorsAsync();
            return professors.Select(MapToDto);
        }

        private static UserDto MapToDto(User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }

        private static string GenerateJwtToken(User user)
        {
            // În producție, aici ai implementa generarea JWT token
            // Pentru moment, returnăm un token simplu
            return $"token_{user.Id}_{DateTime.UtcNow.Ticks}";
        }
    }
} 