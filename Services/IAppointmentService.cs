using clinic_system_be.DTOs;
using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Models;

namespace clinic_system_be.Services
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<IEnumerable<Appointment>>> GetAllAppointments();
        Task<ServiceResponse<Appointment>> GetAppointmentById(int id);
        Task<ServiceResponse<string>> AddAppointment(AddAppointmentDTO appointment);
        Task<ServiceResponse<string>> UpdateAppointment(UpdateAppointmentDTO appointment);
        Task<ServiceResponse<string>> DeleteAppointment(int id);
    }
}
