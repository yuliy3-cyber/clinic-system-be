using clinic_system_be.DTOs.User;
using clinic_system_be.Models;

namespace clinic_system_be.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        Task<bool> PhoneNumberExists(string phoneNumber);
        Task AddUser(AddUserDTO user);
        Task<User> GetUserByEmail(string email);
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
        Task<IEnumerable<User>> GetUsersByRole(string role);
    }
}
