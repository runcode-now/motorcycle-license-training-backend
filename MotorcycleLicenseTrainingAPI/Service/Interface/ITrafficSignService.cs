using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface ITrafficSignervice
    {
        public Task<IEnumerable<TrafficSign>> GetTrafficSignByCategoryId(int categoryId);
    
    }
}
