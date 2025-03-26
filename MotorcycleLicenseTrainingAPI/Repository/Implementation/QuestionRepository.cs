using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;

namespace MotorcycleLicenseTrainingAPI.Repository.Implementation
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MotorcycleLicenseTrainingContext _context;

        public QuestionRepository(MotorcycleLicenseTrainingContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Question>> Delete(int trafficId, QuestionDto trafficDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Question>> GetQuestionByCategoryId(int categoryId)
        {
            try
            {
                var result =  await _context.Question.Where(x => x.CategoryId == categoryId)
                                   .Include(q => q.Answers)
                                   .ToListAsync();
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
            

         

        public Task<IEnumerable<Question>> Update(QuestionDto trafficDto)
        {
            throw new NotImplementedException();
        }
    }
}
