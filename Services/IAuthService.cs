using clinic_system_be.DTOs;

namespace clinic_system_be.Services
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> Register(RegisterDTO registerDTO);
        Task<ServiceResponse<string>> Login(LoginDTO loginDTO);
    }
}
