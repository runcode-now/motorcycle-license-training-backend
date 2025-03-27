using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class TrafficSign
    {
        public int TrafficSignId { get; set; }
        public string? TrafficSignTitle { get; set; }
        public string? ImageUrl { get; set; }
        public string? TrafficSignContent { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
