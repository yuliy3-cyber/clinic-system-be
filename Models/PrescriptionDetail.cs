using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinic_system_be.Models;

[Table("PrescriptionDetail")]
public partial class PrescriptionDetail
{
    [Key]
    public int PrescriptionDetailId { get; set; }

    public int PrescriptionId { get; set; }

    [StringLength(255)]
    public string Medication { get; set; } = null!;

    [StringLength(255)]
    public string Dosage { get; set; } = null!;

    [ForeignKey("PrescriptionId")]
    [InverseProperty("PrescriptionDetails")]
    public virtual Prescription Prescription { get; set; } = null!;
}
