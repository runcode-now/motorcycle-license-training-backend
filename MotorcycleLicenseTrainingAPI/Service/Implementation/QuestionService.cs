using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Service.Implementation
{
    public class Questionervice : IQuestionervice
    {
        private readonly IQuestionRepository _questionRepository;

        public Questionervice(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<IEnumerable<Question>> GetQuestionByCategoryId(int categoryId)
        {
            try
            {
                return await _questionRepository.GetQuestionByCategoryId(categoryId);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
