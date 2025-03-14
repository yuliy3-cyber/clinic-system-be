using clinic_system_be.DTOs;
using clinic_system_be.DTOs.User;
using clinic_system_be.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace clinic_system_be.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Register(RegisterDTO registerDTO)
        {
            if (await _userRepository.UserExists(registerDTO.Email))
            {
                return new ServiceResponse<string> { Success = false, Message = "User already exists." };
            }

            if (await _userRepository.PhoneNumberExists(registerDTO.PhoneNumber))
            {
                return new ServiceResponse<string> { Success = false, Message = "User with this phone number already exists." };
            }

            if (!DateOnly.TryParseExact(registerDTO.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly dateOfBirth))
            {
                return new ServiceResponse<string> { Success = false, Message = "DateOfBirth must be in 'dd/MM/yyyy' format (e.g., 10/03/1990)." };
            }

            bool gender;
            switch (registerDTO.Gender.ToLower())
            {
                case "male":
                    gender = true;
                    break;
                case "female":
                    gender = false;
                    break;
                default:
                    return new ServiceResponse<string> { Success = false, Message = "Gender must be 'Male' or 'Female'." };
            }

            var user = new AddUserDTO
            {
                Email = registerDTO.Email,
                Password = HashPassword(registerDTO.Password),
                FullName = registerDTO.FullName,
                DateOfBirth = dateOfBirth,
                Gender = gender,
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

            //Generate JWT Token
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.UserId.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var preparedToken = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            var token = new JwtSecurityTokenHandler().WriteToken(preparedToken);
            return new ServiceResponse<string> { Success = true, Message = "Login successful.", Data = token };
        }

        public Task<ServiceResponse<string>> Logout()
        {
            // Placeholder for logout logic
            return Task.FromResult(new ServiceResponse<string> { Success = true, Message = "Logout successful." });
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
