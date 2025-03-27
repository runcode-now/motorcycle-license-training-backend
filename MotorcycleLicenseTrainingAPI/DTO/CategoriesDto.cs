using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.DTO
{
    public class CategoryDto
    {

        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Type { get; set; }
    }
}
