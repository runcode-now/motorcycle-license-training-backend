using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;

namespace MotorcycleLicenseTrainingAPI.Repository.Implementation
{
    public class PracticeHistoryRepository : IPracticeHistoryRepository
    {
        private readonly MotorcycleLicenseTrainingContext _context;

        public PracticeHistoryRepository(MotorcycleLicenseTrainingContext context)
        {
            _context = context;
        }

        public async Task<PracticeHistories> CreatePracticeHistoryAsync(PracticeHistories practiceHistory)
        {
            try
            {
                _context.PracticeHistories.Add(practiceHistory);
                await _context.SaveChangesAsync();
                return practiceHistory;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public async Task<PracticeHistories> GetPracticeHistoryByIdAsync(int id)
        {
            return await _context.PracticeHistories.FindAsync(id);
        }

        public async Task<PracticeHistories> UpdatePracticeHistoryAsync(PracticeHistories practiceHistory)
        {
            _context.PracticeHistories.Update(practiceHistory);
            await _context.SaveChangesAsync();
            return practiceHistory;
        }


    }
}
