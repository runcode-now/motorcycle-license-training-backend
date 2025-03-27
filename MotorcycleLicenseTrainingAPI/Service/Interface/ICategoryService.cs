using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetCategoryByType(string type);
    }
}
