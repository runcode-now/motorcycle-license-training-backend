using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class MotorcycleLicenseTrainingContext : IdentityDbContext<ApplicationUsers>
    {
        public MotorcycleLicenseTrainingContext()
        {
        }

        public MotorcycleLicenseTrainingContext(DbContextOptions<MotorcycleLicenseTrainingContext> options)
        : base(options)
        {
        }

        public DbSet<AnswersDto> Answers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<MockExamAnswers> MockExamAnswers { get; set; }
        public DbSet<MockExams> MockExams { get; set; }
        public DbSet<PracticeHistories> PracticeHistories { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<TrafficSigns> TrafficSigns { get; set; }
        public DbSet<ApplicationUsers> ApplicationUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            string connectStr = config.GetConnectionString("DemoConnectStr");
            optionsBuilder.UseSqlServer(connectStr);
        }
    }
}
