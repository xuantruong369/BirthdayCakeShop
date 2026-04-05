namespace BusinessLogic.DTOs
{
    public class AddCustomerDTO
    {
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
    }
}
