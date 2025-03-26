using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;

namespace MotorcycleLicenseTrainingAPI.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MotorcycleLicenseTrainingContext _context;
        public CategoryRepository(MotorcycleLicenseTrainingContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetCategoryByType(string type)
        => await _context.Category.Where(x => x.Type == type)
                                    .ToListAsync();

    }
}
