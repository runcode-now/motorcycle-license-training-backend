using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Service.Interface
{
    public interface IAuthenticationService
    {
        Task<LoginResponse?> LoginUserAsync(LoginRequest request);
        Task<(bool IsSuccess, string Message)> RegisterUserAsync(RegisterRequest request);
    }
}
