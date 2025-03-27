using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Repository.Interface
{
    public interface IAuthenticationRepository
    {
        Task<ApplicationUsers> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<ApplicationUsers?> GetUserByEmailAsync(string email);
        Task AddUserAsync(ApplicationUsers user);
        Task UpdateUserAsync(ApplicationUsers user);
    }
}
