namespace Presentation.ViewModels
{
    public class AdminDashboard
    {
        public int TotalOrderByMonth { get; set; } = 0;
        public int TotalOrderVSLastMonth { get; set; } = 0;
        public decimal TotalRevenueByMonth { get; set; } = 0;
        public decimal TotalRevenueVSLastMonth { get; set; } = 0;
        public int TotalProduct { get; set; } = 0;
        public int TotalNewProductByMonth { get; set; } = 0;
        public int TotalCustomer { get; set; } = 0;
        public int TotalNewCustomerByMonth { get; set; } = 0;
        public decimal[] TotalOrderByEveryMonth { get; set; } = new decimal[12];
        public List<AdminOrderListDash> AdminOrderListDashes { get; set; } = new();
    }
}