using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace clinic_system_be.Models;

[Table("Prescription")]
public partial class Prescription
{
    [Key]
    public int PrescriptionId { get; set; }

    [StringLength(255)]
    public string PrescriptionName { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    [JsonIgnore]
    [InverseProperty("Prescription")]
    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    [InverseProperty("Prescription")]
    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
}
