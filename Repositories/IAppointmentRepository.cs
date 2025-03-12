using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Models;

namespace clinic_system_be.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<Appointment> GetAppointmentById(int id);
        Task AddAppointment(AddAppointmentDTO appointment);
        Task UpdateAppointment(UpdateAppointmentDTO appointment);
        Task DeleteAppointment(int id);
    }
}
