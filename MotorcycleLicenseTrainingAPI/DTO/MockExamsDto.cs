using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.DTO
{
    public class MockExamsDto
    {
        public int TotalScore { get; set; } 

        public bool IsPassed { get; set; } 

        public string UserId { get; set; }

        public string Status { get; set; }
    }
}
