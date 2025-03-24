using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class MockExams
    {
        [Key]
        public int MockExamId { get; set; }

        public DateTime ExamDate { get; set; } = DateTime.Now;

        public int TotalScore { get; set; } // Tổng điểm (ví dụ: 20/25)

        public bool IsPassed { get; set; } // Đậu hay rớt

        public string UserId { get; set; }

        public string Status { get; set; }
        public ApplicationUsers User { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
        public virtual ICollection<MockExamAnswers> MockExamAnswers { get; set; }
    }
}
