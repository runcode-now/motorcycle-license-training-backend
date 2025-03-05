using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Categories>> GetCategoryByType(string type);
    }
}
