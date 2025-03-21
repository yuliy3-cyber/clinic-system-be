using clinic_system_be.DTOs.Appointment;
using clinic_system_be.Services;
using Microsoft.AspNetCore.Mvc;

namespace clinic_system_be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("GetAllAppointments")]
        public async Task<IActionResult> GetAllAppointments(SearchAppointmentDTO model)
        {
            var response = await _appointmentService.GetAllAppointments(model.UserId, model.Status, model.Search, model.From, model.To, model.PageNumber, model.PageSize);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }

        //[HttpGet("GetAllAppointments/{userId}")]
        //public async Task<IActionResult> GetAllAppointments(int userId)
        //{
        //    var response = await _appointmentService.GetAppointmentsByUserId(userId);
        //    if (!response.Success)
        //    {
        //        return BadRequest(response.Message);
        //    }
        //    return Ok(response);
        //}

        [HttpGet("GetAppointmentById/{id}")]
        public async Task<IActionResult> GetAppointmentById(int id)
        {
            var response = await _appointmentService.GetAppointmentById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("AddAppointment")]
        public async Task<IActionResult> AddAppointment([FromBody] AddAppointmentDTO appointment)
        {
            var response = await _appointmentService.AddAppointment(appointment);
            return Ok(response);
        }

        [HttpPut("UpdateAppointment/{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] UpdateAppointmentDTO appointment)
        {
            appointment.AppointmentId = id;
            var response = await _appointmentService.UpdateAppointment(appointment);
            return Ok(response);
        }

        [HttpDelete("DeleteAppointment/{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var response = await _appointmentService.DeleteAppointment(id);
            return Ok(response);
        }
        [HttpPut("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusAppointmentDTO model)
        {
            var response = await _appointmentService.ChangeStatus(model.AppointmentId, model.Status);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }
    }

}
