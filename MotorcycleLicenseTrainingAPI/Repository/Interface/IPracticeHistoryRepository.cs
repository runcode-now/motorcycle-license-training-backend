using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface IPracticeHistoryRepository
    {
        public Task<PracticeHistory> CreatePracticeHistoryAsync(PracticeHistory practiceHistory);
        public Task<PracticeHistory> UpdatePracticeHistoryAsync(PracticeHistory practiceHistory);
        public Task<PracticeHistory> GetPracticeHistoryByIdAsync(int id);
    }
}
