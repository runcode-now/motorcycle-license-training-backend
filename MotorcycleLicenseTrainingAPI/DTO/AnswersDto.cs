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
}
