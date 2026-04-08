using System.Data.Common;

namespace BusinessLogic.DTOs
{
    public class GetDashboardDTO
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
    }
}