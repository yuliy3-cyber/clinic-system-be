using clinic_system_be.Models;
using clinic_system_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace clinic_system_be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpGet("GetAllPrescriptions")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            var response = await _prescriptionService.GetAllPrescriptions();
            return Ok(response);
        }

        [HttpGet("GetPrescriptionById/{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            var response = await _prescriptionService.GetPrescriptionById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("AddPrescription")]
        public async Task<IActionResult> AddPrescription([FromBody] Prescription prescription)
        {
            var response = await _prescriptionService.AddPrescription(prescription);
            return Ok(response);
        }

        [HttpPut("UpdatePrescription/{id}")]
        public async Task<IActionResult> UpdatePrescription(int id, [FromBody] Prescription prescription)
        {
            prescription.PrescriptionId = id;
            var response = await _prescriptionService.UpdatePrescription(prescription);
            return Ok(response);
        }

        [HttpDelete("DeletePrescription/{id}")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            var response = await _prescriptionService.DeletePrescription(id);
            return Ok(response);
        }
    }
}
