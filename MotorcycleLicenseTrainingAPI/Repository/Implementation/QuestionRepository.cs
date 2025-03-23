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

        public Task<IEnumerable<Questions>> Delete(int trafficId, QuestionsDto trafficDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Questions>> GetQuestionByCategoryId(int categoryId)
        {
            try
            {
                var result =  await _context.Questions.Where(x => x.CategoryId == categoryId)
                                   .Include(q => q.Answers)
                                   .ToListAsync();
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
            

         

        public Task<IEnumerable<Questions>> Update(QuestionsDto trafficDto)
        {
            throw new NotImplementedException();
        }
    }
}
