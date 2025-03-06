using clinic_system_be.DTOs;
using clinic_system_be.Models;
using clinic_system_be.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace clinic_system_be.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;

        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<string>> Register(RegisterDTO registerDTO)
        {
            if (await _userRepository.UserExists(registerDTO.Email))
            {
                return new ServiceResponse<string> { Success = false, Message = "User already exists." };
            }

            var user = new User
            {
                Email = registerDTO.Email,
                Password = HashPassword(registerDTO.Password),
                FullName = registerDTO.FullName,
                DateOfBirth = registerDTO.DateOfBirth,
                Gender = registerDTO.Gender,
                Address = registerDTO.Address,
                PhoneNumber = registerDTO.PhoneNumber,
                Status = 1,
                Role = "Patient"
            };

            await _userRepository.AddUser(user);
            return new ServiceResponse<string> { Success = true, Message = "User registered successfully." };
        }

        public async Task<ServiceResponse<string>> Login(LoginDTO loginDTO)
        {
            var user = await _userRepository.GetUserByEmail(loginDTO.Email);
            if (user == null || !VerifyPassword(loginDTO.Password, user.Password))
            {
                return new ServiceResponse<string> { Success = false, Message = "Invalid credentials." };
            }

            // Generate JWT token (omitted for brevity)
            return new ServiceResponse<string> { Success = true, Message = "Login successful.", Data = "JWT Token" };
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }


        private bool VerifyPassword(string password, string hashedPassword)
        {
            return HashPassword(password) == hashedPassword;
        }

    }
}
