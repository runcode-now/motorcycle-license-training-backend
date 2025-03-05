using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class TrafficSigns
    {
        [Key]
        public int TrafficSignId { get; set; }

        public string TrafficSignTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? TrafficSignContent { get; set; }

        [ForeignKey("Categories")]
        public int? CategoryId { get; set; }

        public virtual Categories Category { get; set; }
    }
}
