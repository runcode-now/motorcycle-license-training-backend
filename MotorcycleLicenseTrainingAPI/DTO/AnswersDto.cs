using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.DTO
{
    public class AnswersDto
    {
        public string AnswerText { get; set; } 

        public bool? IsCorrect { get; set; } 

        public int QuestionId { get; set; }

    }

    public class MockExamSubmissionDto
    {
        public int ExamId { get; set; }
        public string UserId { get; set; }
        public List<UserAnswerDto> Answers { get; set; }
    }

    public class UserAnswerDto
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
