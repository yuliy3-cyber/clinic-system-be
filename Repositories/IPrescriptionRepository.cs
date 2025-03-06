using clinic_system_be.Models;

namespace clinic_system_be.Repositories
{
    public interface IPrescriptionRepository
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptions();
        Task<Prescription> GetPrescriptionById(int id);
        Task AddPrescription(Prescription prescription);
        Task UpdatePrescription(Prescription prescription);
        Task DeletePrescription(int id);
    }
}
