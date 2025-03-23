using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface ITrafficSignService
    {
        public Task<IEnumerable<TrafficSigns>> GetTrafficSignByCategoryId(int categoryId);
    
    }
}
