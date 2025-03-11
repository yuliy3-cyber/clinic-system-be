namespace clinic_system_be.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string DateOfBirth { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Address { get; set; }
        public string PhoneNumber { get; set; } = null!;
    }
}