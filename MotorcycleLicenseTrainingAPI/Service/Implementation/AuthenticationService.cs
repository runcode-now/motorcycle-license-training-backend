using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Interface;
using MotorcycleLicenseTrainingAPI.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MotorcycleLicenseTrainingAPI.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authRepository;
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IAuthenticationRepository authRepository, UserManager<ApplicationUsers> userManager, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<LoginResponse?> LoginUserAsync(LoginRequest request)
        {
            try
            {
                var user = await _authRepository.GetUserByEmailAsync(request.Email);
                if (user == null)
                {
                    Console.WriteLine($"User not found for email: {request.Email}");
                    return new LoginResponse { Authenticated = false, Token = string.Empty };
                }

                var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isValidPassword)
                {
                    Console.WriteLine($"Invalid password for user: {request.Email}");
                    return new LoginResponse { Authenticated = false, Token = string.Empty };
                }

                var token = GenerateJwtToken(user);

                return new LoginResponse
                {
                    Token = token,
                    Authenticated = true
                };
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }


        private string GenerateJwtToken(ApplicationUsers user)
        {
            try
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Issuer"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(2),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public async Task<(bool IsSuccess, string Message)> RegisterUserAsync(RegisterRequest request)
        {
            try
            {
                if (await _userManager.FindByEmailAsync(request.Email) != null)
                {
                    return (false, "Email already exists.");
                }

                var user = new ApplicationUsers
                {
                    FullName = request.FullName,
                    UserName = request.Email,
                    Email = request.Email,
                    CreatedAt = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return (false, $"User registration failed: {errors}");
                }

                return (true, "User registered successfully!");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
