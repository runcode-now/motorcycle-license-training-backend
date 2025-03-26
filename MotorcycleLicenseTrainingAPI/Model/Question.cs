using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string? QuestionContent { get; set; }
        public string? ImageUrl { get; set; }
        public string? Reason { get; set; }
        public bool? IsFailing { get; set; }


        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<MockExam> MockExams { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }

    }
}
