using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class Questions
    {
        [Key]
        public int QuestionId { get; set; }

        [Required(ErrorMessage = "Nội dung câu hỏi là bắt buộc")]
        public string QuestionContent { get; set; }

        public string? ImageUrl { get; set; }
        public string? Reason { get; set; }
        public bool? IsFailing { get; set; }

        [ForeignKey("Categories")]
        public int CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual ICollection<MockExams> MockExams { get; set; }
        public virtual ICollection<Answers> Answers { get; set; }
        public virtual ICollection<PracticeHistories> PracticeHistories { get; set; }

    }
}
