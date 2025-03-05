using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Service.Implementation
{
    public class TrafficSignService : ITrafficSignService
    {
        private readonly ITrafficSignRepository _trafficSignRepository;

        public TrafficSignService(ITrafficSignRepository trafficSignRepository)
        {
            _trafficSignRepository = trafficSignRepository;
        }

        public async Task<IEnumerable<TrafficSigns>> GetTrafficSignByCategoryId(int categoryId)
            => await _trafficSignRepository.GetTrafficSignByCategoryId(categoryId);
    }
}
