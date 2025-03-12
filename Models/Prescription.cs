using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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

    [InverseProperty("Prescription")]
    public virtual ICollection<PrescriptionDetail> PrescriptionDetails { get; set; } = new List<PrescriptionDetail>();
}
