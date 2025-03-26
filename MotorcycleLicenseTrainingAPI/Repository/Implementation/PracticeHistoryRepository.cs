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

        public async Task<PracticeHistory> CreatePracticeHistoryAsync(PracticeHistory practiceHistory)
        {
            try
            {
                _context.PracticeHistory.Add(practiceHistory);
                await _context.SaveChangesAsync();
                return practiceHistory;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            
        }

        public async Task<PracticeHistory> GetPracticeHistoryByIdAsync(int id)
        {
            return await _context.PracticeHistory.FindAsync(id);
        }

        public async Task<PracticeHistory> UpdatePracticeHistoryAsync(PracticeHistory practiceHistory)
        {
            _context.PracticeHistory.Update(practiceHistory);
            await _context.SaveChangesAsync();
            return practiceHistory;
        }


    }
}
