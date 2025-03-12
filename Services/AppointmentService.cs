using clinic_system_be.DTOs;
using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Models;
using clinic_system_be.Repositories;

namespace clinic_system_be.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ServiceResponse<IEnumerable<Appointment>>> GetAllAppointments()
        {
            var appointments = await _appointmentRepository.GetAllAppointments();
            return new ServiceResponse<IEnumerable<Appointment>> { Data = appointments, Success = true };
        }

        public async Task<ServiceResponse<Appointment>> GetAppointmentById(int id)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(id);
            if (appointment == null)
            {
                return new ServiceResponse<Appointment> { Success = false, Message = "Appointment not found." };
            }
            return new ServiceResponse<Appointment> { Data = appointment, Success = true };
        }

        public async Task<ServiceResponse<string>> AddAppointment(AddAppointmentDTO appointment)
        {
            await _appointmentRepository.AddAppointment(appointment);
            return new ServiceResponse<string> { Success = true, Message = "Appointment added successfully." };
        }

        public async Task<ServiceResponse<string>> UpdateAppointment(UpdateAppointmentDTO appointment)
        {
            await _appointmentRepository.UpdateAppointment(appointment);
            return new ServiceResponse<string> { Success = true, Message = "Appointment updated successfully." };
        }

        public async Task<ServiceResponse<string>> DeleteAppointment(int id)
        {
            await _appointmentRepository.DeleteAppointment(id);
            return new ServiceResponse<string> { Success = true, Message = "Appointment deleted successfully." };
        }
    }

}
