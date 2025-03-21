using clinic_system_be.DTOs;
using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Models;

namespace clinic_system_be.Services
{
    public interface IAppointmentService
    {
        Task<ServiceResponse<PagedResult<Appointment>>> GetAllAppointments(int userId, int status, string search, DateTime? from, DateTime? to, int pageNumber, int pageSize);
        //Task<ServiceResponse<IEnumerable<Appointment>>> GetAppointments(int userId, int status);
        Task<ServiceResponse<Appointment>> GetAppointmentById(int id);
        Task<ServiceResponse<string>> AddAppointment(AddAppointmentDTO appointment);
        Task<ServiceResponse<string>> UpdateAppointment(UpdateAppointmentDTO appointment);
        Task<ServiceResponse<string>> DeleteAppointment(int id);
        Task<ServiceResponse<string>> ChangeStatus(int appointmentId, int status);

    }
}
