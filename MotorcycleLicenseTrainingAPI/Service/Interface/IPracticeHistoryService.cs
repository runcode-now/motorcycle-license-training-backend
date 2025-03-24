using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface IPracticeHistoryService
    {
        public Task<PracticeHistories> CreatePracticeHistoryAsync(PracticeHistories practiceHistory);
        public Task<PracticeHistories> UpdatePracticeHistoryAsync(PracticeHistories practiceHistory);
        public Task<PracticeHistories> GetPracticeHistoryByIdAsync(int id);
    }
}
