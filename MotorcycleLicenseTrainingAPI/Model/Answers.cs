using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class Answers
    {
        [Key]
        public int AnswerId { get; set; } 

        public string AnswerText { get; set; } 

        public bool? IsCorrect { get; set; } 

        [ForeignKey("Questions")]
        public int QuestionId { get; set; }

        public virtual Questions Question { get; set; }
        public virtual MockExams MockExams { get; set; }

    }
}
