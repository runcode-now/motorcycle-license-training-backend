using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        [StringLength(50, ErrorMessage = "Tên danh mục tối đa 50 ký tự")]
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
        public virtual ICollection<TrafficSigns> TrafficSigns { get; set; }

    }
}
