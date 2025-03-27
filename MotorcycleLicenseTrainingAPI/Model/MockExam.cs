using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class MockExam
    {
        public int MockExamId { get; set; }
        public DateTime? ExamDate { get; set; } = DateTime.Now;
        public int? TotalScore { get; set; } 
        public bool? IsPassed { get; set; }
        public string? Status { get; set; }


        public string UserId { get; set; }
        public ApplicationUsers User { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<MockExamAnswer> MockExamAnswers { get; set; }
    }
}
