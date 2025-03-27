using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface IQuestionervice
    {
        public Task<IEnumerable<Question>> GetQuestionByCategoryId(int categoryId);

    }
}
