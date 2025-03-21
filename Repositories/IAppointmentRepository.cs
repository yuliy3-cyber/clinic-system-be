using clinic_system_be.DTOs;
using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Models;

namespace clinic_system_be.Repositories
{
    public interface IAppointmentRepository
    {
        Task<PagedResult<Appointment>> GetAllAppointments(int userId, int status, string search, DateTime? from, DateTime? to, int pageNumber, int pageSize);
        Task<Appointment> GetAppointmentById(int id);
        Task AddAppointment(AddAppointmentDTO appointment);
        Task UpdateAppointment(Appointment appointment);
        Task UpdateAppointment(UpdateAppointmentDTO appointment);
        Task DeleteAppointment(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int doctorId);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(int patientId);

    }
}
