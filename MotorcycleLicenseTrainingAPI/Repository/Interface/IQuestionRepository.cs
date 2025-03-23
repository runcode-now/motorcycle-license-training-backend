using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface IQuestionRepository
    {
        public Task<IEnumerable<Questions>> GetQuestionByCategoryId(int categoryId);
        public Task<IEnumerable<Questions>> Update(QuestionsDto trafficDto);
        public Task<IEnumerable<Questions>> Delete(int trafficId, QuestionsDto trafficDto);

    }
}
