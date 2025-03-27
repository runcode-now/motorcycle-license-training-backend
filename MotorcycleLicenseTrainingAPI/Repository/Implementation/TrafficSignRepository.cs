using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;

namespace MotorcycleLicenseTrainingAPI.Repository.Implementation
{
    public class TrafficSignRepository : ITrafficSignRepository
    {
        private readonly MotorcycleLicenseTrainingContext _context;

        public TrafficSignRepository(MotorcycleLicenseTrainingContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<TrafficSign>> Delete(int trafficId, TrafficSignDto trafficDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TrafficSign>> GetTrafficSignByCategoryId(int categoryId)
        => await _context.TrafficSign.Where(x => x.CategoryId == categoryId)
                                      .ToListAsync();

        public Task<IEnumerable<TrafficSign>> Update(TrafficSignDto trafficDto)
        {
            throw new NotImplementedException();
        }
    }
}
