namespace Presentation.ViewModels
{
    public class AdminGetCustomers
    {
        public List<CustomerView> customerViews { get; set; } = new();
        public int TotalCustomers { get; set; } = 0;
        public int TotalNewCustomers { get; set; } = 0;
        public int TotalTypeCustomers { get; set; } = 0;
    }
}