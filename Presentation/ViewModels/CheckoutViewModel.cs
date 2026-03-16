using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace Presentation.ViewModels
{
    public class CheckoutViewModel
    {
        public List<OrderItemView> orderItemViews {get; set;} = new List<OrderItemView>();
        
        [Required(ErrorMessage = "Vui lòng nhập nơi giao hàng")]
        [Display(Name = "Nơi giao hàng")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn ngày giao")]
        [Display(Name = "Ngày giao")]
        public DateOnly? DeliveryDate { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn khung giờ giao")]
        [Display(Name = "Khung giờ giao")]
        public string DeliveryTimeSlot { get; set; }

        public string? OrderNote { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        [Display(Name = "Phương thức thanh toán")]
        public string PaymentMethod { get; set; }


    }
}