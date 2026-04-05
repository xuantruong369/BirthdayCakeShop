namespace BusinessLogic.DTOs
{
    public class GetCustomerDTO
    {
        public int CustomerId { get; set; }

        public int? UserId { get; set; }

        public string? CustomerName { get; set; }

        public string? Phone { get; set; }

        public DateOnly? BirthDate { get; set; }

        public string? Address { get; set; }

        public string? Avatar { get; set; }

        public string? CustomerType { get; set; }

        public string Username { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

    }
}