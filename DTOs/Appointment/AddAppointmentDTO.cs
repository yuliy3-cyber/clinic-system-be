using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_system_be.DTOs.Appointment
{
    public class AddAppointmentDTO
    {
        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AppointmentDate { get; set; }
    }
}
