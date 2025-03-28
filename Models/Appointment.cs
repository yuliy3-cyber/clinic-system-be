using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace clinic_system_be.Models;

[Table("Appointment")]
public partial class Appointment
{
    [Key]
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

    public int? PrescriptionId { get; set; }

    //[JsonIgnore]
    [ForeignKey("DoctorId")]
    [InverseProperty("AppointmentDoctors")]
    public virtual User Doctor { get; set; } = null!;

    //[JsonIgnore]
    [ForeignKey("PatientId")]
    [InverseProperty("AppointmentPatients")]
    public virtual User Patient { get; set; } = null!;

    [ForeignKey("PrescriptionId")]
    [InverseProperty("Appointments")]
    public virtual Prescription? Prescription { get; set; }
}
