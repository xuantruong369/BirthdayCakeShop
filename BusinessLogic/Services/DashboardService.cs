using System.Xml;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace BusinessLogic.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICustomerRepository _customerRepo;

        public DashboardService(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
        {
            _orderRepo = orderRepository;
            _productRepo = productRepository;
            _customerRepo = customerRepository;
        }
        // public async Task<GetDashboardDTO> GetDashboard()
        // {
        //     var products = await _productRepo.GetAllProducts();
        //     var cutomers = await _customerRepo.GetAllCustomers();
        //     var orders = await _orderRepo.GetAllOrders();

        //     var date = DateTime.Now;
        //     var lastMonth = date.AddMonths(-1);

        //     return new GetDashboardDTO
        //     {
        //         TotalProduct = products.Count(),
        //         TotalNewProductByMonth = products.Count(p => p.CreatedAt?.Month == date.Month && p.CreatedAt?.Year == date.Year),
        //         TotalCustomer = cutomers.Count(),
        //         TotalNewCustomerByMonth = cutomers.Count(p => p.User.CreatedAt?.Month == date.Month && p.User.CreatedAt?.Year == date.Year),
        //         TotalOrderByMonth = orders.Count(p => p.OrderDate?.Month == date.Month && p.OrderDate?.Year == date.Year),
        //         TotalOrderVSLastMonth = orders.Count(p => p.OrderDate?.Month == lastMonth.Month && p.OrderDate?.Year == lastMonth.Year),
        //         TotalRevenueByMonth = orders.Where(p => p.OrderDate?.Month == date.Month && p.OrderDate?.Year == date.Year).Sum(p => p.OrderItems?.Sum(i => i.TotalPrice) ?? 0),
        //         TotalRevenueVSLastMonth = orders.Where(p => p.OrderDate?.Month == lastMonth.Month && p.OrderDate?.Year == lastMonth.Year).Sum(p => p.OrderItems?.Sum(i => i.TotalPrice) ?? 0)
        //     };
        // }
        // public async Task<GetDashboardDTO> GetDashboard()
        // {
        //     var products = await _productRepo.GetAllProducts();
        //     var customers = await _customerRepo.GetAllCustomers();
        //     var orders = await _orderRepo.GetAllOrders();

        //     var now = DateTime.Now;
        //     var lastMonthDate = now.AddMonths(-1);

        //     // Lọc sẵn danh sách theo tháng để tái sử dụng, giúp code gọn và nhanh hơn
        //     var productsThisMonth = products.Where(p => p.CreatedAt?.Month == now.Month && p.CreatedAt?.Year == now.Year);

        //     var customersThisMonth = customers.Where(c => c.User?.CreatedAt?.Month == now.Month && c.User?.CreatedAt?.Year == now.Year);

        //     var ordersThisMonth = orders.Where(o => o.OrderDate?.Month == now.Month && o.OrderDate?.Year == now.Year).ToList();
        //     var ordersLastMonth = orders.Where(o => o.OrderDate?.Month == lastMonthDate.Month && o.OrderDate?.Year == lastMonthDate.Year).ToList();

        //     var ordersThisYear = orders.Where(o => o.OrderDate?.Year == now.Year).ToList();

        //     var monthlyData = ordersThisYear
        //         .GroupBy(o => o.OrderDate.Value.Month)
        //         .Select(g => new
        //         {
        //             Month = g.Key,
        //             Total = g.Sum(o => o.OrderItems?.Sum(i => i.TotalPrice) ?? 0)
        //         });

        //     var totalOrderByEveryMonth = new decimal[12];

        //     foreach (var item in monthlyData)
        //     {
        //         totalOrderByEveryMonth[item.Month - 1] = item.Total;
        //     }
        //     return new GetDashboardDTO
        //     {
        //         TotalProduct = products.Count(),
        //         TotalNewProductByMonth = productsThisMonth.Count(),

        //         TotalCustomer = customers.Count(),
        //         TotalNewCustomerByMonth = customersThisMonth.Count(),

        //         TotalOrderByMonth = ordersThisMonth.Count,
        //         TotalOrderVSLastMonth = ordersLastMonth.Count,

        //         TotalRevenueByMonth = ordersThisMonth.Sum(o => o.OrderItems?.Sum(i => i.TotalPrice) ?? 0),
        //         TotalRevenueVSLastMonth = ordersLastMonth.Sum(o => o.OrderItems?.Sum(i => i.TotalPrice) ?? 0),
        //         TotalOrderByEveryMonth = totalOrderByEveryMonth
        //     };
        // }
        public async Task<GetDashboardDTO> GetDashboard()
        {
            var products = await _productRepo.GetAllProducts();
            var customers = await _customerRepo.GetAllCustomers();
            var orders = await _orderRepo.GetAllOrders();

            var now = DateTime.Now;
            var lastMonthDate = now.AddMonths(-1);

            // 1. Lọc dữ liệu cơ bản
            var productsThisMonth = products.Where(p => p.CreatedAt?.Month == now.Month && p.CreatedAt?.Year == now.Year);
            var customersThisMonth = customers.Where(c => c.User?.CreatedAt?.Month == now.Month && c.User?.CreatedAt?.Year == now.Year);

            // Dùng .Where(o => o.OrderDate.HasValue) để an toàn khi GroupBy
            var ordersThisYear = orders.Where(o => o.OrderDate?.Year == now.Year && o.OrderDate.HasValue).ToList();

            var ordersThisMonth = ordersThisYear.Where(o => o.OrderDate?.Month == now.Month).ToList();
            var ordersLastMonth = orders.Where(o => o.OrderDate?.Month == lastMonthDate.Month && o.OrderDate?.Year == lastMonthDate.Year).ToList();

            // 2. Tính toán doanh thu 12 tháng (Dùng .Value an toàn vì đã check HasValue ở trên)
            var monthlyData = ordersThisYear
                .GroupBy(o => o.OrderDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(o => o.OrderItems?.Sum(i => i.TotalPrice) ?? 0)
                });

            var totalOrderByEveryMonth = new decimal[12];
            foreach (var item in monthlyData)
            {
                totalOrderByEveryMonth[item.Month - 1] = item.Total;
            }

            // 3. Trả về kết quả
            return new GetDashboardDTO
            {
                TotalProduct = products.Count(),
                TotalNewProductByMonth = productsThisMonth.Count(),

                TotalCustomer = customers.Count(),
                TotalNewCustomerByMonth = customersThisMonth.Count(),

                TotalOrderByMonth = ordersThisMonth.Count,
                TotalOrderVSLastMonth = ordersLastMonth.Count,

                TotalRevenueByMonth = ordersThisMonth.Sum(o => o.OrderItems?.Sum(i => i.TotalPrice) ?? 0),
                TotalRevenueVSLastMonth = ordersLastMonth.Sum(o => o.OrderItems?.Sum(i => i.TotalPrice) ?? 0),

                TotalOrderByEveryMonth = totalOrderByEveryMonth
            };
        }


    }
}