using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.DTO
{
    public class MockExamDto
    {
        public int TotalScore { get; set; } 

        public bool IsPassed { get; set; } 

        public string UserId { get; set; }

        public string Status { get; set; }
    }
}
