using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Presentation.Models;
using Presentation.Models.Authentication;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    [Authentication]
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IVoucherService _voucherService;
        private readonly IDashboardService _dashboardService;

        public AdminController(ICategoryService categoryService, IProductService productService, IOrderService orderService, ICustomerService customerService, IVoucherService voucherService, IDashboardService dashboardService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _orderService = orderService;
            _customerService = customerService;
            _voucherService = voucherService;
            _dashboardService = dashboardService;
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

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAdProducts();
            List<AdminGetProducts> productViews = products.Select(item => new AdminGetProducts
            {
                ProductId = item.ProductId,
                ProductName = item.Name,
                Thumbnail = item.ImageUrl,
                CreatedAt = item.CreatedAt,
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Price = item.Price,
                Stock = item.Stock
            }).ToList();
            return View(productViews);
        }

        public IActionResult Settings()
        {
            return View();
        }

        public async Task<IActionResult> CategoryList()
        {
            var categories = await _categoryService.GetAdminCategorys();
            List<CategoryMenuView> categoryMenuViews = categories.Select(item => new CategoryMenuView
            {
                CategoryId = item.CategoryId,
                CategoryName = item.CategoryName,
                Description = item.Description,
                TotalProducts = item.TotalProducts,
                TotalSold = item.TotalSold
            }).ToList();
            return View(categoryMenuViews);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateEditCategory create)
        {
            if (!ModelState.IsValid)
            {
                return View(create);
            }

            await _categoryService.Add(new GetCategoryDTO
            {
                CategoryName = create.CategoryName,
                Description = create.Description
            });
            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CreateEditCategory edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }

            await _categoryService.Update(new GetCategoryDTO
            {
                CategoryId = edit.CategoryId,
                CategoryName = edit.CategoryName,
                Description = edit.Description
            });
            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public async Task<IActionResult> EditCategory(int categoryId)
        {
            var category = await _categoryService.GetCategoryById(categoryId);
            var categories = new CreateEditCategory
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            };
            return View(categories);
        }


        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                await _categoryService.Delete(categoryId);
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
            return RedirectToAction("CategoryList");
        }

        public async Task<IActionResult> CustomerList()
        {
            var c = await _customerService.GetAdminCustomers();

            List<CustomerView> customers = c.getCustomerDTOs.Select(item => new CustomerView
            {
                CustomerId = item.CustomerId,
                UserId = item.UserId,
                CustomerName = item.CustomerName,
                Phone = item.Phone,
                BirthDate = item.BirthDate,
                Address = item.Address,
                Avatar = item.Avatar,
                CustomerType = item.CustomerType,
                Username = item.Username,
                PasswordHash = item.PasswordHash,
                Role = item.Role,
                CreatedAt = item.CreatedAt
            }).ToList();

            var adminCustomers = new AdminGetCustomers
            {
                customerViews = customers,
                TotalCustomers = c.TotalCustomers,
                TotalNewCustomers = c.TotalNewCustomers,
                TotalTypeCustomers = c.TotalTypeCustomers
            };
            return View(adminCustomers);
        }

        public async Task<IActionResult> DeleteCustomer(int cutomerId)
        {
            try
            {
                await _customerService.DeleteAdminCustomer(cutomerId);
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
            return RedirectToAction("CustomerList");

        }

        public async Task<IActionResult> Dashboard()
        {
            var dash = await _dashboardService.GetDashboard();
            var orders = await _orderService.GetOrders();
            List<AdminOrderListDash> adminOrderList = orders.Select(item => new AdminOrderListDash
            {
                OrderId = item.OrderId,
                CustomerFullName = item.CustomerFullName,
                Phone = item.Phone,
                TotalAmount = item.TotalAmount,
                PaymentMethod = item.PaymentMethod,
                DeliveryAddress = item.DeliveryAddress,
                DeliveryDate = item.DeliveryDate
            }).ToList();
            var dashboard = new AdminDashboard
            {
                TotalOrderByMonth = dash.TotalOrderByMonth,
                TotalOrderVSLastMonth = dash.TotalOrderVSLastMonth,
                TotalRevenueByMonth = dash.TotalRevenueByMonth,
                TotalRevenueVSLastMonth = dash.TotalRevenueVSLastMonth,
                TotalProduct = dash.TotalProduct,
                TotalNewProductByMonth = dash.TotalNewProductByMonth,
                TotalCustomer = dash.TotalCustomer,
                TotalNewCustomerByMonth = dash.TotalNewCustomerByMonth,
                TotalOrderByEveryMonth = dash.TotalOrderByEveryMonth,
                AdminOrderListDashes = adminOrderList
            };
            return View(dashboard);
        }

        public IActionResult Reports()
        {
            return View();
        }

        public async Task<IActionResult> VoucherList()
        {
            var vouchers = await _voucherService.GetAllVouchers();
            List<AdminGetVoucherItem> voucheritem = vouchers.voucherItemDTOs.Select(item => new AdminGetVoucherItem
            {
                VoucherId = item.VoucherId,
                VoucherCode = item.VoucherCode,
                DiscountValue = item.DiscountValue,
                IsPercentage = item.IsPercentage,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                strIsPercentage = (item.IsPercentage == true) ? "Số tiền cố định" : "Phần trăm",
                strIsActive = (item.EndDate >= DateTime.Now) ? "Hoạt động" : "Hết hạn"
            }).ToList();

            var voucherView = new AdminGetVouchers
            {
                adminGetVoucherItems = voucheritem,
                TotalVoucherActive = vouchers.TotalVoucherActive,
                TotalVoucherNoActive = vouchers.TotalVoucherNoActive
            };
            return View(voucherView);
        }

        [HttpGet]
        public IActionResult CreateVoucher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVoucher(CreateEditVoucher create)
        {
            if (!ModelState.IsValid)
            {
                return View(create);
            }

            var isExist = await _voucherService.CheckVoucherCodeExist(create.VoucherCode);
            if (isExist)
            {
                // "VoucherCode" phải trùng khớp với tên thuộc tính trong Model để hiện lỗi đúng chỗ
                ModelState.AddModelError("VoucherCode", "Mã voucher này đã tồn tại trong hệ thống.");
                return View(create);
            }
            await _voucherService.AddVoucher(new VoucherItemDTO
            {
                VoucherCode = create.VoucherCode,
                DiscountValue = create.DiscountValue,
                IsPercentage = create.IsPercentage,
                StartDate = create.StartDate,
                EndDate = create.EndDate
            });
            return RedirectToAction("VoucherList");
        }

        public async Task<IActionResult> DeleteVoucher(int voucherId)
        {
            try
            {
                await _voucherService.DeleteVoucher(voucherId);
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
            return RedirectToAction("VoucherList");

        }

        [HttpPost]
        public async Task<IActionResult> EditVoucher(CreateEditVoucher edit)
        {
            if (!ModelState.IsValid)
            {
                return View(edit);
            }

            var isExist = await _voucherService.CheckVoucherCodeExist(edit.VoucherCode);
            if (isExist)
            {
                // "VoucherCode" phải trùng khớp với tên thuộc tính trong Model để hiện lỗi đúng chỗ
                ModelState.AddModelError("VoucherCode", "Mã voucher này đã tồn tại trong hệ thống.");
                return View(edit);
            }

            await _voucherService.EditVoucher(new VoucherItemDTO
            {
                VoucherId = edit.VoucherId,
                VoucherCode = edit.VoucherCode,
                DiscountValue = edit.DiscountValue,
                IsPercentage = edit.IsPercentage,
                StartDate = edit.StartDate,
                EndDate = edit.EndDate
            });
            return RedirectToAction("VoucherList");
        }

        [HttpGet]
        public async Task<IActionResult> EditVoucher(int voucherId)
        {
            var voucher = await _voucherService.GetVoucherById(voucherId);
            var voucherEdit = new CreateEditVoucher
            {
                VoucherId = voucher.VoucherId,
                VoucherCode = voucher.VoucherCode,
                DiscountValue = voucher.DiscountValue,
                IsPercentage = voucher.IsPercentage,
                StartDate = voucher.StartDate,
                EndDate = voucher.EndDate
            };
            return View(voucherEdit);
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

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int productId)
        {
            // var categories = await _categoryService.GetCategorys();

            // var vm = new CreateProduct
            // {
            //     Categories = categories.Select(c => new SelectListItem
            //     {
            //         Value = c.CategoryId.ToString(),
            //         Text = c.CategoryName
            //     }).ToList()
            // };

            var categories = await _categoryService.GetCategorys();
            var product = await _productService.GetAllProductById(productId);
            var vm = new EditProduct
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                CakeSize = product.CakeSize,
                Flavor = product.Flavor,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList()

            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProduct model)
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

            await _productService.UpdateAllProduct(new AdminProductDTO
            {
                ProductId = model.ProductId,
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                Description = model.Description,
                Thumbnail = thumbnailFileName,
                Images = images,
                Price = model.Price,
                CakeSize = model.CakeSize,
                Flavor = model.Flavor
            });

            return RedirectToAction("Index");
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
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> DeleteMultiple([FromBody] List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Json(new { success = false, message = "Không có sản phẩm nào được chọn" });
            }

            try
            {
                foreach (var productId in ids)
                {
                    await _productService.DeleteProduct(productId);
                }

                return Json(new { success = true, message = "Xóa thành công" });
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Không thể xóa vì sản phẩm đang nằm trong giỏ hàng/đơn hàng"
                    });
                }

                return Json(new
                {
                    success = false,
                    message = "Lỗi hệ thống khi xóa"
                });
            }
        }


    }
}