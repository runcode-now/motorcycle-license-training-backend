using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? Type { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<TrafficSign> TrafficSigns { get; set; }

    }
}
