using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.DTO;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface ITrafficSignRepository
    {
        public Task<IEnumerable<TrafficSign>> GetTrafficSignByCategoryId(int categoryId);
        public Task<IEnumerable<TrafficSign>> Update(TrafficSignDto trafficDto);
        public Task<IEnumerable<TrafficSign>> Delete(int trafficId, TrafficSignDto trafficDto);
    }
}
