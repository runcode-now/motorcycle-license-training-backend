using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.DTO
{
    public class TrafficSignDto
    {
        public string TrafficSignTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? TrafficSignContent { get; set; }
        public int? CategoryId { get; set; }
    }
}
