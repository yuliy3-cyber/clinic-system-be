using clinic_system_be.DTOs;
using clinic_system_be.Models;

namespace clinic_system_be.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<User>>> GetAllUsers();
        Task<ServiceResponse<User>> GetUserById(int id);
        Task<ServiceResponse<string>> AddUser(User user);
        Task<ServiceResponse<string>> UpdateUser(User user);
        Task<ServiceResponse<string>> DeleteUser(int id);
    }
}
