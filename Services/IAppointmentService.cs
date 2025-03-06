using clinic_system_be.DTOs;
using clinic_system_be.Models;

namespace clinic_system_be.Services
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<IEnumerable<Appointment>>> GetAllAppointments();
        Task<ServiceResponse<Appointment>> GetAppointmentById(int id);
        Task<ServiceResponse<string>> AddAppointment(Appointment appointment);
        Task<ServiceResponse<string>> UpdateAppointment(Appointment appointment);
        Task<ServiceResponse<string>> DeleteAppointment(int id);
    }
}
