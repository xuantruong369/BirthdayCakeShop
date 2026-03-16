using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class RegisterUserView
{
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string PasswordHash { get; set; } = null!;

    [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Họ và tên không được để trống")]
    public string CustomerName { get; set; } = null!;

    [Required(ErrorMessage = "Số điện thoại không được để trống")]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Địa chỉ không được để trống")]
    public string Address { get; set; } = null!;

    public string Role { get; set; } = "Customer";
}
}