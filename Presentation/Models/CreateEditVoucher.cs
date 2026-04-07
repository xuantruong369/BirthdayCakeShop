using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class CreateEditVoucher : IValidatableObject
    {
        [Key]
        public int VoucherId { get; set; }

        [Required(ErrorMessage = "Mã voucher không được để trống")]
        public string VoucherCode { get; set; } = null!;

        [Required(ErrorMessage = "Giá trị giảm giá không được để trống")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá trị giảm giá phải lớn hơn 0")]
        public decimal DiscountValue { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại giảm giá")]
        public bool? IsPercentage { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime? EndDate { get; set; }

        // Logic kiểm tra điều kiện StartDate < EndDate
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (StartDate.HasValue && EndDate.HasValue)
            {
                if (StartDate.Value >= EndDate.Value)
                {
                    yield return new ValidationResult(
                        "Ngày bắt đầu phải nhỏ hơn ngày kết thúc",
                        new[] { nameof(StartDate), nameof(EndDate) }
                    );
                }
            }
        }
    }
}