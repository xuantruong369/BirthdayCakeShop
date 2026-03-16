namespace BusinessLogic.DTOs
{
    public class AddUserDTO
    {
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Role { get; set; } = "Customer";
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string ConfirmPassword { get; set; } = null!;
    }
}