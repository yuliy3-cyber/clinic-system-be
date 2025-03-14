using System.ComponentModel.DataAnnotations;

namespace clinic_system_be.DTOs.User
{
    public class AddUserDTO
    {
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

    }
}
