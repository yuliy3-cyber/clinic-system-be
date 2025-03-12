using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Models;
using Microsoft.EntityFrameworkCore;

namespace clinic_system_be.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ClinicSystemDbContext _context;

        public AppointmentRepository(ClinicSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentById(int id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task AddAppointment(AddAppointmentDTO appointment)
        {
            var newAppointment = new Appointment
            {
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Status = 1,
            };
            await _context.Appointments.AddAsync(newAppointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointment(UpdateAppointmentDTO appointment)
        {
            var newAppointment = new Appointment
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status,
                Diagnosis = appointment.Diagnosis,
                Description = appointment.Description,
            };
            _context.Appointments.Update(newAppointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }

}
