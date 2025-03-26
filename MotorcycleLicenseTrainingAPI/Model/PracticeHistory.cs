using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class PracticeHistory
    {
        public int PracticeHistoryId { get; set; }
        public int? AnswerId { get; set; } 
        public bool? IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        public ApplicationUsers User { get; set; }
    }
}
