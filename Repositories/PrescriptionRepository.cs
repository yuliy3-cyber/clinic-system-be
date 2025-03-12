using clinic_system_be.Models;
using Microsoft.EntityFrameworkCore;

namespace clinic_system_be.Repositories
{
    public class PrescriptionRepository : IPrescriptionRepository
    {
        private readonly ClinicSystemDbContext _context;

        public PrescriptionRepository(ClinicSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prescription>> GetAllPrescriptions()
        {
            return await _context.Prescriptions
                .Include(p => p.PrescriptionDetails)
                .ToListAsync();
        }

        public async Task<Prescription> GetPrescriptionById(int id)
        {
            return await _context.Prescriptions
                .Include(p => p.PrescriptionDetails)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
        }

        public async Task AddPrescription(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePrescription(Prescription prescription)
        {
            _context.Prescriptions.Update(prescription);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePrescription(int id)
        {
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
                await _context.SaveChangesAsync();
            }
        }
    }

}
