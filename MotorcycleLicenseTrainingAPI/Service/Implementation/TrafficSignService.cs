using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Service.Implementation
{
    public class TrafficSignervice : ITrafficSignervice
    {
        private readonly ITrafficSignRepository _trafficSignRepository;

        public TrafficSignervice(ITrafficSignRepository trafficSignRepository)
        {
            _trafficSignRepository = trafficSignRepository;
        }

        public async Task<IEnumerable<TrafficSign>> GetTrafficSignByCategoryId(int categoryId)
            => await _trafficSignRepository.GetTrafficSignByCategoryId(categoryId);
    }
}
