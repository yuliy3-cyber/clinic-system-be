using clinic_system_be.DTOs;
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

        public async Task<PagedResult<Appointment>> GetAllAppointments(int userId, int status, string search, DateTime? from, DateTime? to, int pageNumber, int pageSize)
        {
            var query = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .AsQueryable();

            if (status != 0)
            {
                query = query.Where(a => a.Status == status);
            }

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => a.Doctor.FullName.Contains(search) || a.Patient.FullName.Contains(search));
            }

            if (from.HasValue)
            {
                query = query.Where(a => a.AppointmentDate.Date >= from.Value.Date);
            }

            if (to.HasValue)
            {
                query = query.Where(a => a.AppointmentDate.Date <= to.Value.Date);
            }

            var user = await _context.Users.FindAsync(userId);
            if (user.Role != "Admin")
            {
                query = query.Where(a => a.DoctorId == userId || a.PatientId == userId);
            }

            var totalRecords = await query.CountAsync();
            var appointments = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Appointment>
            {
                Items = appointments,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
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
        public async Task UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
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
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctorId(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(int patientId)
        {
            return await _context.Appointments
                .Where(a => a.PatientId == patientId)
                .Include(a => a.Doctor)
                .ToListAsync();
        }
    }

}
