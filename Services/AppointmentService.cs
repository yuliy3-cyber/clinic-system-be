using clinic_system_be.DTOs;
using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Models;
using clinic_system_be.Repositories;
using Microsoft.EntityFrameworkCore;

namespace clinic_system_be.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUserRepository _userRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IUserRepository userRepository)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
        }
        public async Task<ServiceResponse<string>> ChangeStatus(int appointmentId, int status)
        {
            var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                return new ServiceResponse<string> { Success = false, Message = "Appointment not found" };
            }

            appointment.Status = status;

            try
            {
                _appointmentRepository.UpdateAppointment(appointment);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return new ServiceResponse<string> { Success = true, Message = "Status updated successfully" };
        }
        public async Task<ServiceResponse<PagedResult<Appointment>>> GetAllAppointments(int userId, int status, string search, DateTime? from, DateTime? to, int pageNumber, int pageSize)
        {
            var appointments = await _appointmentRepository.GetAllAppointments(userId, status, search, from, to, pageNumber, pageSize);
            return new ServiceResponse<PagedResult<Appointment>> { Data = appointments, Success = true };
        }
        //public async Task<ServiceResponse<IEnumerable<Appointment>>> GetAppointments(int userId, int status)
        //{
        //    var user = await _userRepository.GetUserById(userId);
        //    if (user == null)
        //    {
        //        return new ServiceResponse<IEnumerable<Appointment>> { Data = null, Success = false, Message = "User not found" };
        //    }

        //    IEnumerable<Appointment> appointments = await _appointmentRepository.GetAllAppointments(userId, status);
        //    //if (user.Role == "Admin")
        //    //{
        //    //    appointments = await _appointmentRepository.GetAllAppointments(userId, status);
        //    //}
        //    //else if (user.Role == "Doctor")
        //    //{
        //    //    appointments = await _appointmentRepository.GetAppointmentsByDoctorId(userId);
        //    //}
        //    //else if (user.Role == "Patient")
        //    //{
        //    //    appointments = await _appointmentRepository.GetAppointmentsByPatientId(userId);
        //    //}
        //    //else
        //    //{
        //    //    return new ServiceResponse<IEnumerable<Appointment>> { Data = null, Success = false, Message = "Invalid user role" };
        //    //}
        //    return new ServiceResponse<IEnumerable<Appointment>> { Data = appointments, Success = true };
        //}

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
