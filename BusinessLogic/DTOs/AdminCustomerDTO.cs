namespace BusinessLogic.DTOs
{
    public class AdminCustomerDTO
    {
        public List<GetCustomerDTO> getCustomerDTOs { get; set; } = new();
        public int TotalCustomers { get; set; } = 0;
        public int TotalNewCustomers { get; set; } = 0;
        public int TotalTypeCustomers { get; set; } = 0;
    }
}