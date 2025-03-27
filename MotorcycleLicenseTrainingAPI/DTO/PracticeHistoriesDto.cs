using MotorcycleLicenseTrainingAPI.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.DTO
{
    public class PracticeHistoryDto
    {
        public int AnswerId { get; set; }

        public bool IsCorrect { get; set; }

        public string UserId { get; set; }

        public int QuestionId { get; set; }
    }
}
