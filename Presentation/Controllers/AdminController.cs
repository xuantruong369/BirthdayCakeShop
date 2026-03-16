using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        public AdminController(ICategoryService categoryService, IProductService productService, IOrderService orderService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
        }
        public async Task<IActionResult> OrderList()
        {
            var orders = await _orderService.GetOrders();

            List<OrderListItemViewModel> orderListItemViewModels = orders.Select(item => new OrderListItemViewModel
            {
                OrderId = item.OrderId,
                CustomerFullName = item.CustomerFullName,
                Phone = item.Phone,
                OrderDate = item.OrderDate,
                TotalAmount = item.TotalAmount,
                PaymentMethod = item.PaymentMethod,
                DeliveryAddress = item.DeliveryAddress,
                DeliveryDate = item.DeliveryDate,
                DeliveryTimeSlot = item.DeliveryTimeSlot,
                OrderNote = item.OrderNote,
                Products = item.Products.Select(i => new OrderProductItemViewModel
                {
                    ProductName = i.ProductName,
                    ProductImage = i.ProductImage,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    TotalPrice = i.TotalPrice
                }).ToList()
            }).ToList();
            return View(orderListItemViewModels);
        }
        public IActionResult Index()
        {
            return RedirectToAction("ProductList");
        }
        public async Task<IActionResult> ProductList()
        {
            var products = await _productService.GetAdProducts();

            List<ProductAdminView> productAdminViews = products.Select(item => new ProductAdminView
            {
                ProductId = item.ProductId,
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                Price = item.Price,
                ShortDescription = item.ShortDescription,
                CategoryName = item.CategoryName
            }).ToList();
            return View(productAdminViews);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            var categories = await _categoryService.GetCategorys();

            var vm = new CreateProduct
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProduct model)
        {
            var categories = await _categoryService.GetCategorys();
            model.Categories = categories.Select(c => new SelectListItem
            {
                Value = c.CategoryId.ToString(),
                Text = c.CategoryName
            }).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            if (model.Thumbnail == null || model.Thumbnail.Length == 0)
            {
                ModelState.AddModelError("Thumbnail", "Vui lòng chọn ảnh thumbnail");
                return View(model);
            }

            if (model.Images == null || !model.Images.Any())
            {
                ModelState.AddModelError("Images", "Vui lòng chọn ít nhất 1 ảnh chi tiết");
                return View(model);
            }


            // ===== Lưu 1 ảnh thumbnail =====
            var thumbnailFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Client",
                "img",
                "product"
            );

            if (!Directory.Exists(thumbnailFolder))
            {
                Directory.CreateDirectory(thumbnailFolder);
            }

            string thumbnailFileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Thumbnail.FileName);
            var thumbnailPath = Path.Combine(thumbnailFolder, thumbnailFileName);

            using (var stream = new FileStream(thumbnailPath, FileMode.Create))
            {
                await model.Thumbnail.CopyToAsync(stream);
            }

            // ===== Lưu nhiều ảnh detail =====
            var detailFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "Client",
                "img",
                "product",
                "details"
            );

            if (!Directory.Exists(detailFolder))
            {
                Directory.CreateDirectory(detailFolder);
            }

            List<string> images = new List<string>();

            foreach (var file in model.Images)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(detailFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                images.Add(fileName);
            }

            await _productService.AddProduct(new AddProductDTO
            {
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                Description = model.Description,
                Thumbnail = thumbnailFileName,
                Images = images,
                Price = model.Price,
                CakeSize = model.CakeSize,
                Flavor = model.Flavor
            });

            return RedirectToAction("ProductList");
        }

        public IActionResult GetProducts()
        {
            return View();
        }

        public async Task<IActionResult> DeleteProduct(int productId)
        {
            

            try
            {
                await _productService.DeleteProduct(productId);
                TempData["Success"] = "Xóa thành công";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    ModelState.AddModelError("", "Không thể xóa sản phẩm này vì nó đang nằm trong giỏ hàng của khách hàng.");
                    // Hoặc dùng TempData để hiển thị thông báo ra View
                    TempData["Error"] = "Xóa thất bại: Sản phẩm này đang có dữ liệu liên quan (giỏ hàng/đơn hàng).";
                }
                else
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi hệ thống khi xóa. Vui lòng thử lại.");
                }
            }
            return RedirectToAction("ProductList");
        
        }
    }
}