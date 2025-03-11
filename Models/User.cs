using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace clinic_system_be.Models
{
    [Table("User")]
    [Index("PhoneNumber", Name = "UQ__User__85FB4E380A6B1265", IsUnique = true)]
    [Index("Email", Name = "UQ__User__A9D105340F3194EB", IsUnique = true)]
    public partial class User
    {
        [Key]
        public int UserId { get; set; }

        [StringLength(255)]
        public string Email { get; set; } = null!;

        [StringLength(255)]
        public string Password { get; set; } = null!;

        [StringLength(255)]
        public string FullName { get; set; } = null!;

        public DateOnly DateOfBirth { get; set; }

        public bool Gender { get; set; }

        [StringLength(500)]
        public string? Address { get; set; }

        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;

        public int Status { get; set; }

        [StringLength(50)]
        public string Role { get; set; } = null!;

        [InverseProperty("Doctor")]
        public virtual ICollection<Appointment> AppointmentDoctors { get; set; } = new List<Appointment>();

        [InverseProperty("Patient")]
        public virtual ICollection<Appointment> AppointmentPatients { get; set; } = new List<Appointment>();
    }
}