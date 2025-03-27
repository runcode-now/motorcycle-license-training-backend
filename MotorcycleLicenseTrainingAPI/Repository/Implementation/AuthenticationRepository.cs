using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;

namespace MotorcycleLicenseTrainingAPI.Repository.Implementation
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;

        public AuthenticationRepository(UserManager<ApplicationUsers> userManager)
        {
            _userManager = userManager;
        }
        public Task AddUserAsync(ApplicationUsers user)
        {
            throw new NotImplementedException();
        }


        public Task<ApplicationUsers> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ApplicationUsers> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task UpdateUserAsync(ApplicationUsers user)
        {
            throw new NotImplementedException();
        }
    }
}
