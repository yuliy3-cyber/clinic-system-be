using clinic_system_be.DTOs;
using clinic_system_be.Models;
using clinic_system_be.Repositories;

namespace clinic_system_be.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public async Task<ServiceResponse<IEnumerable<Prescription>>> GetAllPrescriptions()
        {
            var prescriptions = await _prescriptionRepository.GetAllPrescriptions();
            return new ServiceResponse<IEnumerable<Prescription>> { Data = prescriptions, Success = true };
        }

        public async Task<ServiceResponse<Prescription>> GetPrescriptionById(int id)
        {
            var prescription = await _prescriptionRepository.GetPrescriptionById(id);
            if (prescription == null)
            {
                return new ServiceResponse<Prescription> { Success = false, Message = "Prescription not found." };
            }
            return new ServiceResponse<Prescription> { Data = prescription, Success = true };
        }

        public async Task<ServiceResponse<string>> AddPrescription(Prescription prescription)
        {
            await _prescriptionRepository.AddPrescription(prescription);
            return new ServiceResponse<string> { Success = true, Message = "Prescription added successfully." };
        }

        public async Task<ServiceResponse<string>> UpdatePrescription(Prescription prescription)
        {
            await _prescriptionRepository.UpdatePrescription(prescription);
            return new ServiceResponse<string> { Success = true, Message = "Prescription updated successfully." };
        }

        public async Task<ServiceResponse<string>> DeletePrescription(int id)
        {
            await _prescriptionRepository.DeletePrescription(id);
            return new ServiceResponse<string> { Success = true, Message = "Prescription deleted successfully." };
        }
    }
}
