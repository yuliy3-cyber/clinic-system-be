using clinic_system_be.DTOs;
using clinic_system_be.Models;

namespace clinic_system_be.Services
{
    public interface IPrescriptionService
    {
        Task<ServiceResponse<IEnumerable<Prescription>>> GetAllPrescriptions();
        Task<ServiceResponse<Prescription>> GetPrescriptionById(int id);
        Task<ServiceResponse<string>> AddPrescription(Prescription prescription);
        Task<ServiceResponse<string>> UpdatePrescription(Prescription prescription);
        Task<ServiceResponse<string>> DeletePrescription(int id);
    }

}
