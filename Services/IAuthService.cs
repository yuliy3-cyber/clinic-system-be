using clinic_system_be.DTOs;

namespace clinic_system_be.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(RegisterDTO registerDTO);
        Task<ServiceResponse<object>> Login(LoginDTO loginDTO);
        Task<ServiceResponse<string>> Logout();
        Task<bool> IsUserInactive(string email);
    }
}
