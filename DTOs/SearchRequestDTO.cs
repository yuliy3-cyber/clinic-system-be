namespace clinic_system_be.DTOs
{
    public class SearchRequestDTO
    {
        public string Search { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
