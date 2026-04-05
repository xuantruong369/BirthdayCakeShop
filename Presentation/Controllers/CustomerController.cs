using System.Threading.Tasks;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels;

namespace Presentation.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomerController(ICustomerService customerService, IUserService userService)
        {
            _customerService = customerService;
            _userService = userService;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            var c = await _customerService.GetCustomerByUserId(userId);
            var customer = new CustomerView
            {
                CustomerId = c.CustomerId,
                UserId = c.UserId,
                CustomerName = c.CustomerName,
                Phone = c.Phone,
                BirthDate = c.BirthDate,
                Address = c.Address,
                Avatar = c.Avatar,
                CustomerType = c.CustomerType,
                Username = c.Username,
                PasswordHash = c.PasswordHash,
                Role = c.Role,
                CreatedAt = c.CreatedAt
            };
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(CustomerView model, IFormFile avatarFile)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("Id");
                if (!userId.HasValue || userId != model.UserId)
                {
                    return Unauthorized();
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(model.CustomerName) ||
                    string.IsNullOrWhiteSpace(model.Phone) ||
                    string.IsNullOrWhiteSpace(model.Address))
                {
                    ModelState.AddModelError("", "Vui lòng nhập đầy đủ các trường bắt buộc!");
                    return View(model);
                }

                // Handle avatar upload
                if (avatarFile != null && avatarFile.Length > 0)
                {
                    // Validate file size (max 2MB)
                    if (avatarFile.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("", "Kích thước ảnh không được vượt quá 2MB!");
                        return View(model);
                    }

                    // Validate file type
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(avatarFile.FileName).ToLower();
                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        ModelState.AddModelError("", "Chỉ hỗ trợ các định dạng: JPG, JPEG, PNG, GIF!");
                        return View(model);
                    }

                    try
                    {
                        // ===== Lưu 1 ảnh thumbnail =====
                        var thumbnailFolder = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "Client",
                            "img"
                        );

                        if (!Directory.Exists(thumbnailFolder))
                        {
                            Directory.CreateDirectory(thumbnailFolder);
                        }

                        string thumbnailFileName = Guid.NewGuid().ToString() + Path.GetExtension(avatarFile.FileName);
                        var thumbnailPath = Path.Combine(thumbnailFolder, thumbnailFileName);

                        using (var stream = new FileStream(thumbnailPath, FileMode.Create))
                        {
                            await avatarFile.CopyToAsync(stream);
                        }

                        model.Avatar = thumbnailFileName;
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Lỗi khi tải lên ảnh: " + ex.Message);
                        return View(model);
                    }
                }

                // Create DTO for service
                var addCustomerDTO = new AddCustomerDTO
                {
                    CustomerName = model.CustomerName,
                    Phone = model.Phone,
                    BirthDate = model.BirthDate,
                    Address = model.Address,
                    Avatar = model.Avatar
                };

                // Update customer through service
                bool success = await _customerService.UpdateCustomer(model.CustomerId, addCustomerDTO);
                if (success)
                {
                    TempData["Success"] = "Cập nhật hồ sơ thành công!";
                    return RedirectToAction("Profile");
                }

                ModelState.AddModelError("", "Lỗi khi cập nhật hồ sơ!");
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(int customerId, string currentPassword, string newPassword, string confirmPassword)
        {
            try
            {
                if (newPassword != confirmPassword)
                {
                    TempData["Error"] = "Mật khẩu xác nhận không khớp!";
                    return RedirectToAction("Profile");
                }

                if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
                {
                    TempData["Error"] = "Vui lòng nhập đầy đủ các trường!";
                    return RedirectToAction("Profile");
                }

                var userId = HttpContext.Session.GetInt32("Id");
                if (!userId.HasValue)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var user = await _userService.GetUserById(userId);

                // Verify current password (should use hashing in real app)
                if (currentPassword != user.PasswordHash)
                {
                    TempData["Error"] = "Mật khẩu hiện tại không đúng!";
                    return RedirectToAction("Profile");
                }

                user.PasswordHash = newPassword;
                await _userService.UpdatePassword(user);

                TempData["Success"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("Profile");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi: " + ex.Message;
                return RedirectToAction("Profile");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePreferences(int customerId, bool notifyOrders, bool notifyPromotions, string language, string currency)
        {
            // Implement preference update logic
            TempData["Success"] = "Cập nhật cài đặt thành công!";
            return RedirectToAction("Profile");
        }

        // Admin APIs
        [HttpGet]
        public async Task<IActionResult> GetCustomerDetails(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                if (customer == null)
                {
                    return Json(new { success = false, message = "Khách hàng không tồn tại" });
                }

                return Json(new
                {
                    success = true,
                    customer = new
                    {
                        customerId = customer.CustomerId,
                        username = customer.Username,
                        customerName = customer.CustomerName,
                        phone = customer.Phone,
                        birthDate = customer.BirthDate?.ToString("yyyy-MM-dd"),
                        address = customer.Address,
                        avatar = customer.Avatar,
                        customerType = customer.CustomerType,
                        role = customer.Role,
                        createdAt = customer.CreatedAt?.ToString("yyyy-MM-dd HH:mm")
                    }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                bool success = await _customerService.DeleteCustomer(id);
                if (success)
                {
                    return Json(new { success = true, message = "Xóa khách hàng thành công" });
                }
                return Json(new { success = false, message = "Không thể xóa khách hàng" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}