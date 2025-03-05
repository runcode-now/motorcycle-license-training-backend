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

        public Task<IEnumerable<TrafficSigns>> Delete(int trafficId, TrafficSignsDto trafficDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TrafficSigns>> GetTrafficSignByCategoryId(int categoryId)
        => await _context.TrafficSigns.Where(x => x.CategoryId == categoryId)
                                      .ToListAsync();

        public Task<IEnumerable<TrafficSigns>> Update(TrafficSignsDto trafficDto)
        {
            throw new NotImplementedException();
        }
    }
}
