using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class MockExamAnswer
    {
        public int MockExamAnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public int? AnswerId { get; set; }
        public int QuestionId { get; set; }

        public int MockExamId { get; set; }
        public virtual MockExam MockExam { get; set; }
    }
}