using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MotorcycleLicenseTrainingAPI.Mapper;
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
            services.AddScoped<IMockExamsSerivce, MockExamsSerivce>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPracticeHistoryService, PracticeHistoryService>();

            services.AddScoped<ITrafficSignRepository, TrafficSignRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IPracticeHistoryRepository, PracticeHistoryRepository>();
            services.AddScoped<IMockExamsReposioty, MockExamsReposioty>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IPracticeHistoryRepository, PracticeHistoryRepository>();
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

            // Authentication setup for JWT Bearer tokens
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");

            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });


            // Create interface in swagger
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Motorcycle License Training API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Nhập token theo định dạng: Bearer <JWT>"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddAuthorization();


            // Add AutoMapper
            builder.Services.AddAutoMapper(typeof(Program));

            // CORS configuration
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", builder =>
                builder.WithOrigins("http://127.0.0.1:5500")
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            // Swagger configuration for API documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add controllers to the container
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            builder.Services.AddAutoMapper(typeof(PracticeHistoryProfile));


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
