using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface IPracticeHistoryService
    {
        public Task<PracticeHistory> CreatePracticeHistoryAsync(PracticeHistory practiceHistory);
        public Task<PracticeHistory> UpdatePracticeHistoryAsync(PracticeHistory practiceHistory);
        public Task<PracticeHistory> GetPracticeHistoryByIdAsync(int id);
    }
}
