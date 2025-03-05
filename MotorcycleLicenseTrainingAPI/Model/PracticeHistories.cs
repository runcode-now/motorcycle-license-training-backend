using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class PracticeHistories
    {
        [Key]
        public int PracticeHistoryId { get; set; }

        public int AnswerId { get; set; } 

        [Required(ErrorMessage = "Result is required")]
        public bool IsCorrect { get; set; } 

        public string UserId { get; set; }
        public ApplicationUsers User { get; set; }

        [ForeignKey("Questions")]
        public int QuestionId { get; set; }

        public virtual Questions Question { get; set; }
    }
}
