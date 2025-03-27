using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Repository.Implementation;
using MotorcycleLicenseTrainingAPI.Repository.Interface;
using MotorcycleLicenseTrainingAPI.Service.Implementation;
using MotorcycleLicenseTrainingAPI.Service.Interface;
using System.Text;

namespace MotorcycleLicenseTrainingAPI
{
    public class Program
    {
        private static void InjectService(IServiceCollection services)
        {
            services.AddScoped<ITrafficSignService, TrafficSignService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<ITrafficSignRepository, TrafficSignRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddIdentity<ApplicationUsers, IdentityRole>()
                            .AddEntityFrameworkStores<MotorcycleLicenseTrainingContext>()
                            .AddDefaultTokenProviders();

            // Read connection string from appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("DemoConnectStr");

            // Add DbContext to DI container
            builder.Services.AddDbContext<MotorcycleLicenseTrainingContext>(options =>
                options.UseSqlServer(connectionString));

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));

            // CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            builder.Services.AddControllers();

            // Swagger configuration for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Authentication setup for JWT Bearer tokens
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:ValidAudience"],
                    ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])
                    )
                };
            });

            // Inject custom services
            InjectService(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply CORS policy
            app.UseCors("AllowLocalhost");

            // Middleware for Authorization and Routing
            app.UseAuthorization();

            // Mapping controllers
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
