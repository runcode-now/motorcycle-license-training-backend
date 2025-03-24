using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class MockExamAnswers
    {
        [Key]
        public int MockExamAnswerId { get; set; }

        [Required(ErrorMessage = "Result is required")]
        public bool IsCorrect { get; set; }

        public int? AnswerId { get; set; }

        [ForeignKey("MockExams")]
        public int MockExamId { get; set; }

        public int QuestionId { get; set; }

        public virtual MockExams MockExam { get; set; }
    }
}