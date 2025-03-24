using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface IPracticeHistoryRepository
    {
        public Task<PracticeHistories> CreatePracticeHistoryAsync(PracticeHistories practiceHistory);
        public Task<PracticeHistories> UpdatePracticeHistoryAsync(PracticeHistories practiceHistory);
        public Task<PracticeHistories> GetPracticeHistoryByIdAsync(int id);
    }
}
