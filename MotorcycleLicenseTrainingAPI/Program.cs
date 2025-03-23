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
using System.Text.Json.Serialization;

namespace MotorcycleLicenseTrainingAPI
{
    public class Program
    {
        // Method to inject custom services
        private static void InjectService(IServiceCollection services)
        {
            services.AddScoped<ITrafficSignService, TrafficSignService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddScoped<ITrafficSignRepository, TrafficSignRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add DbContext to DI container
            var connectionString = builder.Configuration.GetConnectionString("DemoConnectStr");
            builder.Services.AddDbContext<MotorcycleLicenseTrainingContext>(options =>
                options.UseSqlServer(connectionString));

            // Add Identity to DI container
            builder.Services.AddIdentity<ApplicationUsers, IdentityRole>()
                            .AddEntityFrameworkStores<MotorcycleLicenseTrainingContext>()
                            .AddDefaultTokenProviders();

            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", builder =>
                    builder.WithOrigins("http://127.0.0.1:5500")  // Allowing this origin
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            // Swagger configuration for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Authentication setup for JWT Bearer tokens

            builder.Services.AddAuthentication().AddJwtBearer();





            // Add controllers to the container
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            }); ;

            // Inject custom services
            InjectService(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Apply CORS policy
            app.UseCors("AllowLocalhost");

            // Middleware for Authorization and Routing

            app.UseAuthentication();
            app.UseAuthorization();




            // Map controllers to the pipeline
            app.MapControllers();

            // Run the application
            app.Run();
        }
    }
}
