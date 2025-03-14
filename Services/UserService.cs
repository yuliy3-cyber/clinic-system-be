using clinic_system_be.DTOs;
using clinic_system_be.DTOs.User;
using clinic_system_be.Models;
using clinic_system_be.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace clinic_system_be.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return new ServiceResponse<IEnumerable<User>> { Data = users, Success = true };
        }

        public async Task<ServiceResponse<User>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return new ServiceResponse<User> { Success = false, Message = "User not found." };
            }
            return new ServiceResponse<User> { Data = user, Success = true };
        }

        public async Task<ServiceResponse<string>> AddUser(AddUserDTO user)
        {
            if (await _userRepository.UserExists(user.Email))
            {
                return new ServiceResponse<string> { Success = false, Message = "User already exists." };
            }
            if (await _userRepository.PhoneNumberExists(user.PhoneNumber))
            {
                return new ServiceResponse<string> { Success = false, Message = "User with this phone number already exists." };
            }

            user.Password = HashPassword(user.Password);
            await _userRepository.AddUser(user);
            return new ServiceResponse<string> { Success = true, Message = "User added successfully." };
        }

        public async Task<ServiceResponse<string>> UpdateUser(User user)
        {
            await _userRepository.UpdateUser(user);
            return new ServiceResponse<string> { Success = true, Message = "User updated successfully." };
        }

        public async Task<ServiceResponse<string>> DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
            return new ServiceResponse<string> { Success = true, Message = "User deleted successfully." };
        }
        public async Task<ServiceResponse<IEnumerable<User>>> GetUsersByRole(string role)
        {
            var users = await _userRepository.GetUsersByRole(role);
            return new ServiceResponse<IEnumerable<User>> { Data = users, Success = true };
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
