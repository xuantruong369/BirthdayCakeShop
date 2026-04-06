
using System.ComponentModel.DataAnnotations;

namespace Presentation.Models
{
    public class CreateEditCategory
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100, ErrorMessage = "Tối đa 100 ký tự")]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string? Description { get; set; }
    }
}