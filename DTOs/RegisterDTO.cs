namespace clinic_system_be.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
    }
}
