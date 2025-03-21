namespace clinic_system_be.DTOs.Appointment
{
    public class SearchAppointmentDTO : SearchRequestDTO
    {
        public int UserId { get; set; }
        public int Status { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}
