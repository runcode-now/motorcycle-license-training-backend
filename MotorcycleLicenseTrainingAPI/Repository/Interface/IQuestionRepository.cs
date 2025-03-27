using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface IQuestionRepository
    {
        public Task<IEnumerable<Question>> GetQuestionByCategoryId(int categoryId);
        public Task<IEnumerable<Question>> Update(QuestionDto trafficDto);
        public Task<IEnumerable<Question>> Delete(int trafficId, QuestionDto trafficDto);

    }
}
