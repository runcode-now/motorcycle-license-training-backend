using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface IQuestionService
    {
        public Task<IEnumerable<Questions>> GetQuestionByCategoryId(int categoryId);

    }
}
