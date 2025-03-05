using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Categories>> GetCategoryByType(string type);
    }
}
