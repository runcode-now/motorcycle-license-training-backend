using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.DTO;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface ITrafficSignRepository
    {
        public Task<IEnumerable<TrafficSigns>> GetTrafficSignByCategoryId(int categoryId);
        public Task<IEnumerable<TrafficSigns>> Update(TrafficSignsDto trafficDto);
        public Task<IEnumerable<TrafficSigns>> Delete(int trafficId, TrafficSignsDto trafficDto);
    }
}
