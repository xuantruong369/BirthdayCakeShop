using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Models
{
    public class CreateProduct
    {
        [Required(ErrorMessage = "Vui lòng chọn thể loại")]
        public int? CategoryId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn ảnh đại diện")]
        public IFormFile Thumbnail { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng chọn ít nhất 1 ảnh chi tiết")]
        public List<IFormFile> Images { get; set; } = new();

        [Required(ErrorMessage = "Vui lòng nhập giá")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        public string? CakeSize { get; set; }

        public string? Flavor { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();
    }
}