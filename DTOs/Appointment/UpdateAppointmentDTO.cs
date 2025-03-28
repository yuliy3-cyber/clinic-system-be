using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_system_be.DTOs.Appointment
{
    public class UpdateAppointmentDTO
    {
        public int AppointmentId { get; set; }

        public int PatientId { get; set; }

        public int DoctorId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AppointmentDate { get; set; }
        [Column(TypeName = "text")]
        public string? Diagnosis { get; set; }

        public int Status { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        public int PrescriptionId { get; set; }
    }
}
