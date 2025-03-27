using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class Answer
    {
        public int AnswerId { get; set; } 
        public string? AnswerText { get; set; } 
        public bool? IsCorrect { get; set; } 

        
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

    }
}
