using Microsoft.AspNetCore.Identity;
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

        public DbSet<Answers> Answers { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gọi phương thức OnModelCreating của lớp cha (IdentityDbContext)
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính composite cho IdentityUserLogin<string>
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });

            // Cấu hình khóa chính composite cho IdentityUserRole<string>
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            // Cấu hình khóa chính composite cho IdentityUserToken<string>
            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            // Cấu hình quan hệ nhiều-nhiều giữa Questions và MockExams (để tránh lỗi trước đó)
            modelBuilder.Entity<Questions>()
                .HasMany(q => q.MockExams)
                .WithMany(me => me.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "MockExamsQuestions",
                    j => j.HasOne<MockExams>().WithMany().HasForeignKey("MockExamId"),
                    j => j.HasOne<Questions>().WithMany().HasForeignKey("QuestionId"),
                    j =>
                    {
                        j.HasKey("MockExamId", "QuestionId");
                    });

            // Cấu hình quan hệ 1-nhiều giữa Questions và Answers
            modelBuilder.Entity<Questions>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
