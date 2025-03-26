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

        public DbSet<Answer> Answer { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<MockExamAnswer> MockExamAnswer { get; set; }
        public DbSet<MockExam> MockExam { get; set; }
        public DbSet<PracticeHistory> PracticeHistory { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<TrafficSign> TrafficSign { get; set; }
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

            // 1. Cấu hình quan hệ nhiều-nhiều giữa Question và MockExam
            modelBuilder.Entity<Question>()
                .HasMany(q => q.MockExams)
                .WithMany(me => me.Questions)
                .UsingEntity<Dictionary<string, object>>(
                    "MockExamQuestion", // Tên bảng trung gian
                    j => j.HasOne<MockExam>().WithMany().HasForeignKey("MockExamId"),
                    j => j.HasOne<Question>().WithMany().HasForeignKey("QuestionId"),
                    j =>
                    {
                        j.HasKey("MockExamId", "QuestionId"); // Khóa chính composite
                    });

            // 2. Cấu hình quan hệ 1-nhiều giữa ApplicationUsers và PracticeHistory
            modelBuilder.Entity<PracticeHistory>()
                .HasOne(ph => ph.User)
                .WithMany(u => u.PracticeHistories)
                .HasForeignKey(ph => ph.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa lịch sử khi xóa user

            // 3. Cấu hình quan hệ 1-nhiều giữa ApplicationUsers và MockExam
            modelBuilder.Entity<MockExam>()
                .HasOne(me => me.User)
                .WithMany(u => u.MockExams)
                .HasForeignKey(me => me.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa bài thi khi xóa user

            // 4. Cấu hình quan hệ 1-nhiều giữa Question và Answer
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa câu trả lời khi xóa câu hỏi

            // 5. Cấu hình quan hệ 1-nhiều giữa Category và Question
            modelBuilder.Entity<Question>()
                .HasOne(q => q.Category)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa danh mục nếu có câu hỏi

            // 6. Cấu hình quan hệ 1-nhiều giữa Category và TrafficSign
            modelBuilder.Entity<TrafficSign>()
                .HasOne(ts => ts.Category)
                .WithMany(c => c.TrafficSigns)
                .HasForeignKey(ts => ts.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa danh mục nếu có biển báo

            // 7. Cấu hình quan hệ 1-nhiều giữa MockExam và MockExamAnswer
            modelBuilder.Entity<MockExamAnswer>()
                .HasOne(mea => mea.MockExam)
                .WithMany(me => me.MockExamAnswers)
                .HasForeignKey(mea => mea.MockExamId)
                .OnDelete(DeleteBehavior.Cascade); // Xóa câu trả lời khi xóa bài thi

            // 8. Cấu hình quan hệ 1-nhiều giữa Question và PracticeHistory
            modelBuilder.Entity<PracticeHistory>()
                .HasOne(ph => ph.Question)
                .WithMany() // Không cần navigation property ngược lại
                .HasForeignKey(ph => ph.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa câu hỏi nếu có lịch sử

            // 9. Cấu hình quan hệ 0-1 giữa Answer và PracticeHistory
            modelBuilder.Entity<PracticeHistory>()
                .HasOne(ph => ph.Answer)
                .WithMany() // Không cần navigation property ngược lại
                .HasForeignKey(ph => ph.AnswerId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa câu trả lời nếu có lịch sử

            // 10. Cấu hình quan hệ 1-nhiều giữa Question và MockExamAnswer
            modelBuilder.Entity<MockExamAnswer>()
                .HasOne(mea => mea.Question)
                .WithMany() // Không cần navigation property ngược lại
                .HasForeignKey(mea => mea.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa câu hỏi nếu có câu trả lời bài thi

            // 11. Cấu hình quan hệ 0-1 giữa Answer và MockExamAnswer
            modelBuilder.Entity<MockExamAnswer>()
                .HasOne(mea => mea.Answer)
                .WithMany() // Không cần navigation property ngược lại
                .HasForeignKey(mea => mea.AnswerId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa câu trả lời nếu đã chọn trong bài thi



            // Seeding dữ liệu
            // 1. ApplicationUsers (AspNetUsers)
            modelBuilder.Entity<ApplicationUsers>().HasData(
                new ApplicationUsers
                {
                    Id = "77c46877-537a-4637-bafe-b76b21a2f56e",
                    UserName = "demo11@gmail.com",
                    NormalizedUserName = "DEMO11@GMAIL.COM",
                    Email = "demo11@gmail.com",
                    FullName = "string",
                    CreatedAt = DateTime.Parse("2025-03-25T00:04:42.6079841"),
                    PasswordHash = "AQAAAAEAACcQAAAAENcEUydoUtT/B7gQw9O0TMdMvVZ11kOL7BXsa428YPOG84sJRcW1fcOTuYyN+xzmFQ=="
                }
            );

            // 2. Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Biển báo cấm", Type = "traffic_sign" },
                new Category { CategoryId = 2, CategoryName = "Biển hiệu lệnh", Type = "traffic_sign" },
                new Category { CategoryId = 3, CategoryName = "Biển chỉ dẫn", Type = "traffic_sign" },
                new Category { CategoryId = 4, CategoryName = "Biển báo nguy hiểm và cảnh báo", Type = "traffic_sign" },
                new Category { CategoryId = 5, CategoryName = "Biển phụ", Type = "traffic_sign" },
                new Category { CategoryId = 6, CategoryName = "Khái niệm và quy tắc", ImageUrl = "https://png.pngtree.com/png-vector/20220925/ourmid/pngtree-a-man-explains-list-of-rule-guidelines-png-image_6212794.png", Type = "theory" },
                new Category { CategoryId = 7, CategoryName = "Văn hóa và đạo đức lái xe", ImageUrl = "https://trungtamanhngu.net/wp-content/uploads/2023/04/3.png", Type = "theory" },
                new Category { CategoryId = 8, CategoryName = "Kỹ thuật lái xe", ImageUrl = "https://lambanglaixe.vn/wp-content/uploads/2017/12/logo-745x800.png", Type = "theory" },
                new Category { CategoryId = 9, CategoryName = "Biển báo đường bộ", ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-410.jpg?v=1600845714683", Type = "theory" },
                new Category { CategoryId = 10, CategoryName = "Sa hình", ImageUrl = "https://img.onthibanglaixe.net/blog/2023/5/kich-thuoc-vong-so-8-1688134433403.jpg", Type = "theory" },
                new Category { CategoryId = 11, CategoryName = "Cảnh báo", ImageUrl = "https://cdn-icons-png.flaticon.com/512/1410/1410040.png", Type = "theory" }
            );

            // 3. MockExams
            modelBuilder.Entity<MockExam>().HasData(
                new MockExam { MockExamId = 9, ExamDate = DateTime.Parse("2025-04-01"), TotalScore = 90, IsPassed = true, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 10, ExamDate = DateTime.Parse("2025-04-02"), TotalScore = 75, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 11, ExamDate = DateTime.Parse("2025-04-03"), TotalScore = 80, IsPassed = true, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 12, ExamDate = DateTime.Parse("2025-12-01"), TotalScore = 1, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "completed" },
                new MockExam { MockExamId = 13, ExamDate = DateTime.Parse("2025-03-24T10:00:00"), TotalScore = 85, IsPassed = true, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 14, ExamDate = DateTime.Parse("2025-03-25T14:30:00"), TotalScore = 60, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 15, ExamDate = DateTime.Parse("2025-03-24T21:41:21.0562617"), TotalScore = -1, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 16, ExamDate = DateTime.Parse("2025-03-24T21:44:19.4920969"), TotalScore = -1, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 17, ExamDate = DateTime.Parse("2025-03-25T06:32:08.4968371"), TotalScore = -1, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 18, ExamDate = DateTime.Parse("2025-03-25T06:32:08.4995312"), TotalScore = 1, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "completed" },
                new MockExam { MockExamId = 19, ExamDate = DateTime.Parse("2025-03-25T07:46:56.4925216"), TotalScore = -1, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" },
                new MockExam { MockExamId = 20, ExamDate = DateTime.Parse("2025-03-25T11:39:50.1589201"), TotalScore = -1, IsPassed = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", Status = "not_started" }
            );

            // 4. Questions
            modelBuilder.Entity<Question>().HasData(
                new Question { QuestionId = 1, QuestionContent = "Người hành nghề lái xe khi thực hiện tốt việc rèn luyện, nâng cao trách nhiệm, đạo đức nghề nghiệp sẽ thu được kết quả như thế nào?", IsFailing = true, CategoryId = 7 },
                new Question { QuestionId = 2, QuestionContent = "Người lái xe vận tải hàng hóa cần thực hiện những nhiệm vụ gì ghi ở dưới đây?", IsFailing = false, CategoryId = 7 },
                new Question { QuestionId = 3, QuestionContent = "Người lái xe kinh doanh vận tải cần thực hiện những công việc gì ghi ở dưới đây để thường xuyên rèn luyện nâng cao đạo đức nghề nghiệp?", Reason = "Tín hiệu vàng báo hiệu xe phải dừng trước vạch dừng; nếu đã vượt qua vạch thì được đi tiếp.", IsFailing = false, CategoryId = 7 },
                new Question { QuestionId = 4, QuestionContent = "Người lái xe và nhân viên phục vụ trên xe ô tô vận tải hành khách phải có những trách nhiệm gì theo quy định được ghi ở dưới đây?", Reason = "Theo quy định, lái xe sử dụng rượu bia sẽ bị phạt hành chính, tước bằng lái, nghiêm trọng có thể truy cứu trách nhiệm hình sự.", IsFailing = true, CategoryId = 7 },
                new Question { QuestionId = 5, QuestionContent = "Khái niệm về văn hóa giao thông được hiểu như thế nào là đúng?", Reason = "Tín hiệu vàng yêu cầu người lái xe dừng lại trước vạch dừng, trừ khi đã vượt qua vạch thì được tiếp tục đi.", IsFailing = false, CategoryId = 7 },
                new Question { QuestionId = 11, QuestionContent = "Biển nào cấm ô tô tải?", ImageUrl = "https://lythuyet.onthigplx.vn/images/onthigplx_vn__q306.webp", Reason = "Không có", IsFailing = false, CategoryId = 9 },
                new Question { QuestionId = 12, QuestionContent = "Phần của đường bộ được sử dụng cho các phương tiện giao thông qua lại là gì?", IsFailing = false, CategoryId = 6 },
                new Question { QuestionId = 13, QuestionContent = "“Làn đường” là gì?", IsFailing = false, CategoryId = 6 },
                new Question { QuestionId = 14, QuestionContent = "Khái niệm “Khổ giới hạn đường bộ” được hiểu như thế nào là đúng?", IsFailing = false, CategoryId = 6 },
                new Question { QuestionId = 15, QuestionContent = "Trong các khái niệm dưới đây, “dải phân cách” được hiểu như thế nào là đúng?", IsFailing = false, CategoryId = 6 },
                new Question { QuestionId = 16, QuestionContent = "Biển báo hiệu có dạng hình tròn, viền đỏ, nền trắng, trên nền có hình vẽ hoặc chữ số, chữ viết màu đen là loại biển gì dưới đây?", ImageUrl = "https://thibanglaixe24h.net/wp-content/uploads/2020/08/bien-1.png", Reason = "Biển cấm: vòng tròn đỏ.", IsFailing = false, CategoryId = 9 },
                new Question { QuestionId = 17, QuestionContent = "Biển báo hiệu có dạng tam giác đều, viền đỏ, nền màu vàng, trên có hình vẽ màu đen là loại biển gì dưới đây?", ImageUrl = "https://thibanglaixe24h.net/wp-content/uploads/2020/08/bien-2.png", IsFailing = false, CategoryId = 9 }
            );

            // 5. PracticeHistories
            modelBuilder.Entity<PracticeHistory>().HasData(
                new PracticeHistory { PracticeHistoryId = 1, AnswerId = 24, IsCorrect = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 12 },
                new PracticeHistory { PracticeHistoryId = 2, AnswerId = 21, IsCorrect = true, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 11 },
                new PracticeHistory { PracticeHistoryId = 3, AnswerId = 7, IsCorrect = true, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 1 },
                new PracticeHistory { PracticeHistoryId = 4, AnswerId = 26, IsCorrect = true, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 13 },
                new PracticeHistory { PracticeHistoryId = 5, AnswerId = 33, IsCorrect = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 14 },
                new PracticeHistory { PracticeHistoryId = 6, AnswerId = 34, IsCorrect = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 15 },
                new PracticeHistory { PracticeHistoryId = 7, AnswerId = 11, IsCorrect = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 2 },
                new PracticeHistory { PracticeHistoryId = 8, AnswerId = 12, IsCorrect = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 3 },
                new PracticeHistory { PracticeHistoryId = 9, AnswerId = 39, IsCorrect = false, UserId = "77c46877-537a-4637-bafe-b76b21a2f56e", QuestionId = 5 }
            );

            // 6. TrafficSigns
            modelBuilder.Entity<TrafficSign>().HasData(
                new TrafficSign { TrafficSignId = 1, TrafficSignTitle = "Biển số P.124a \"Cấm quay đầu xe\"", ImageUrl = "https://lh4.googleusercontent.com/proxy/p-X43Y8E4jtza7YzFM6FbBDmQY2yMWKK6ZX5EvRToCqk1B9z79nPcmhw109LJJeRvf-iSkLQPhnUoLV1pu22cPNP_4o", TrafficSignContent = "Để báo cấm các loại xe quay đầu (theo kiểu chữ U). Chiều mũi tên phù hợp với chiều cấm quay đầu xe. Biển số P124a có hiệu lực cấm các loại xe (có giới và thổ sơ) trừ các xe được ưu tiên theo quy định. Biển không có giá trị cấm rẽ trái để đi sang hướng đường khác.", CategoryId = 1 },
                new TrafficSign { TrafficSignId = 2, TrafficSignTitle = "Biển số P105 \"Cấm xe ô tô và xe máy\"", ImageUrl = "https://bizweb.dktcdn.net/100/352/036/files/bien-bao-105.jpg?v=1575706005214", TrafficSignContent = "Để báo đường cấm các loại xe có giới và xe máy đi qua.", CategoryId = 1 },
                new TrafficSign { TrafficSignId = 3, TrafficSignTitle = "Biển số P101 \"Đường cấm\"", ImageUrl = "https://myanhsafety.com.vn/image/cache/catalog/bien-bao-giao-thong-duong-cam-p-101-1-500x500.jpg", TrafficSignContent = "Để báo đường cấm các loại phương tiện đi lại cả hai hướng, trừ các xe được ưu tiên theo quy định.", CategoryId = 1 },
                new TrafficSign { TrafficSignId = 4, TrafficSignTitle = "Biển số P102 \"Cấm đi ngược chiều\"", ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/22578292-db52d-jpeg.jpg?v=1600507890367", TrafficSignContent = "Để báo đường cấm các loại xe (có giới và thổ sơ) đi vào theo chiều đặt biển, trừ các xe được ưu tiên theo quy định. Người đi bộ được phép đi trên vỉa hè hoặc lề đường.", CategoryId = 1 },
                new TrafficSign { TrafficSignId = 6, TrafficSignTitle = "Biển số P103a \"Cấm xe ô tô\"", ImageUrl = "https://giaothongmiennam.com/uploads/source/loaibienbao/103a.jpg", TrafficSignContent = "Để báo đường cấm các loại xe có giới kể cả xe máy 3 bánh có thùng đi qua, trừ xe máy 2 bánh, xe gắn máy và các xe được ưu tiên theo quy định.", CategoryId = 1 },
                new TrafficSign { TrafficSignId = 7, TrafficSignTitle = "Biển số I.401 \"Bắt đầu đường ưu tiên\"", ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-401.jpg?v=1600847944553", TrafficSignContent = "Biểu thị ưu tiên cho các phương tiện trên đường có đặt biển này được đi trước.", CategoryId = 3 },
                new TrafficSign { TrafficSignId = 8, TrafficSignTitle = "Biển số I.402 \"Hết đoạn đường ưu tiên\"", ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-402.jpg?v=1600846599747", TrafficSignContent = "Biểu thị hết đoạn đường quy định là ưu tiên.", CategoryId = 3 },
                new TrafficSign { TrafficSignId = 9, TrafficSignTitle = "Biển số I.405a \"Đường cụt\"", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-81QbPq3MXpSKC9-L1J8Sromeeo2jQRxzYw&s", TrafficSignContent = "Chỉ lối rẽ vào đường cụt.", CategoryId = 3 },
                new TrafficSign { TrafficSignId = 10, TrafficSignTitle = "Biển số I.405b \"Đường cụt\"", ImageUrl = "https://lh5.googleusercontent.com/proxy/R2QonNrldpzKmuP0W4wWKziXU4Yb5R08kL3XBFfzOuHwsmJm79K3pnfFZ8XqV-XIZPDV00QIrUcIIYPeEzqPbxITMUE", TrafficSignContent = "Chỉ lối rẽ vào đường cụt.", CategoryId = 3 },
                new TrafficSign { TrafficSignId = 11, TrafficSignTitle = "Biển số R122 \"Dừng lại\"", ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-dung-lai.jpg?v=1599812231573", TrafficSignContent = "Để báo các xe (có giới và thổ sơ) dừng lại.", CategoryId = 2 },
                new TrafficSign { TrafficSignId = 12, TrafficSignTitle = "Biển số R.301a \"Hướng đi phải theo\"", ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/5748397301-jpeg.jpg?v=1599903632117", TrafficSignContent = "Khi đặt biển số R.301a ở trước nơi đường giao nhau thì hiệu lực tác dụng của biển là ở phạm vi khu vực nơi đường giao nhau phía sau biển tức là cấm xe rẽ phải hay rẽ trái. Nếu biển này đặt ở sau nơi đường giao nhau (bắt đầu vào đoạn đường phố) thì hiệu lực tác dụng của biển là từ vị trí đặt biển đến nơi đường giao nhau. Trong trường hợp này cấm rẽ trái và quay đầu trong vùng tác dụng của biển, chỉ cho phép rẽ phải vào cổng nhà hoặc ngõ phố có trên đoạn đường từ nơi đường giao nhau đặt biển đến nơi đường giao nhau tiếp theo.", CategoryId = 2 },
                new TrafficSign { TrafficSignId = 13, TrafficSignTitle = "Biển số R.301b \"Hướng đi phải theo\"", ImageUrl = "https://bizweb.dktcdn.net/thumb/1024x1024/100/352/036/products/1831909bien-hieu-lenh-301e-jpeg.jpg?v=1599903903743", TrafficSignContent = "Nhằm chỉ hướng cho phép xe đi ngang qua nơi đường giao nhau và ngăn chặn hướng đi ngược chiều trên đường phố với đường một chiều. Biển bắt buộc người tham gia giao thông chỉ được phép rẽ phải hoặc rẽ trái ở phạm vi nơi đường giao nhau trước mặt biển.", CategoryId = 2 },
                new TrafficSign { TrafficSignId = 14, TrafficSignTitle = "Biển số W.201a \"Chỗ ngoặt nguy hiểm\"", ImageUrl = "https://myanhsafety.com.vn/image/cache/catalog/bien-bao-nguy-hiem-201a-1-0x0.jpg", TrafficSignContent = "Báo trước sắp đến một chỗ ngoặt nguy hiểm vòng bên trái.", CategoryId = 4 },
                new TrafficSign { TrafficSignId = 15, TrafficSignTitle = "Biển số W.201b \"Chỗ ngoặt nguy hiểm\"", ImageUrl = "https://myanhsafety.com.vn/image/cache/catalog/bien-bao-nguy-hiem-201b-500x500.jpg", TrafficSignContent = "Báo trước sắp đến một chỗ ngoặt nguy hiểm vòng bên phải.", CategoryId = 4 },
                new TrafficSign { TrafficSignId = 16, TrafficSignTitle = "Biển số W.202a \"Nhiều chỗ ngoặt nguy hiểm liên tiếp\"", ImageUrl = "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-202.jpg?v=1600248249870", TrafficSignContent = "Báo trước sắp đến hai chỗ ngoặt ngược chiều nhau liên tiếp, ở gần nhau trong đó có ít nhất một chỗ ngoặt nguy hiểm.", CategoryId = 4 },
                new TrafficSign { TrafficSignId = 17, TrafficSignTitle = "Biển số S.501 \"Phạm vi tác dụng của biển\"", ImageUrl = "https://cdn-i.vtcnews.vn/resize/th/upload/2024/05/03/bien-bao-501-1-15032563.jpg", TrafficSignContent = "Để thông báo chiều dài đoạn đường nguy hiểm hoặc cấm hoặc hạn chế bên dưới một số biển báo nguy hiểm, biển báo cấm hoặc hạn chế.", CategoryId = 5 },
                new TrafficSign { TrafficSignId = 18, TrafficSignTitle = "Biển số S.502 \"Khoảng cách đến đối tượng báo hiệu\"", ImageUrl = "https://sathachmophong.com/wp-content/uploads/2022/12/Bien-so-S.502.webp", TrafficSignContent = "Để thông báo khoảng cách thực tế từ vị trí đặt biển đến đối tượng báo hiệu ở phía trước. Con số trên biển ghi theo đơn vị mét (m) và lấy chân đến hàng chức mét.", CategoryId = 5 },
                new TrafficSign { TrafficSignId = 19, TrafficSignTitle = "Biển số S.503a \"Hướng tác dụng của biển\"", ImageUrl = "https://lh5.googleusercontent.com/proxy/R5rT_cBAkJKAsB4nCjDP9xvGAOo1GZ9WLasPUDU2SxD2DaSSxVTWtyaAZ26X_dfqDTxk2d9UjP7eBpsW6PK9tpi88Y0Ra1EPpIUtOOksXw5wxCxM1hHcwxpFxGtsr1Gf-6K4RUQc8m1iKwPMDYw", TrafficSignContent = "Để chỉ hướng tác dụng của biển là hướng vuông góc với chiều đi.", CategoryId = 5 }
            );

            // 8. MockExamsQuestions (bảng trung gian)
            modelBuilder.Entity("MockExamQuestion").HasData(
                new { MockExamId = 9, QuestionId = 1 },
                new { MockExamId = 10, QuestionId = 1 },
                new { MockExamId = 12, QuestionId = 1 },
                new { MockExamId = 17, QuestionId = 1 },
                new { MockExamId = 18, QuestionId = 1 },
                new { MockExamId = 19, QuestionId = 1 },
                new { MockExamId = 20, QuestionId = 1 },
                new { MockExamId = 10, QuestionId = 2 },
                new { MockExamId = 12, QuestionId = 2 },
                new { MockExamId = 17, QuestionId = 2 },
                new { MockExamId = 18, QuestionId = 2 },
                new { MockExamId = 19, QuestionId = 2 },
                new { MockExamId = 20, QuestionId = 2 },
                new { MockExamId = 12, QuestionId = 3 },
                new { MockExamId = 17, QuestionId = 3 },
                new { MockExamId = 18, QuestionId = 3 },
                new { MockExamId = 19, QuestionId = 3 },
                new { MockExamId = 20, QuestionId = 3 },
                new { MockExamId = 12, QuestionId = 4 },
                new { MockExamId = 17, QuestionId = 4 },
                new { MockExamId = 18, QuestionId = 4 },
                new { MockExamId = 19, QuestionId = 4 },
                new { MockExamId = 20, QuestionId = 4 },
                new { MockExamId = 12, QuestionId = 5 },
                new { MockExamId = 17, QuestionId = 5 },
                new { MockExamId = 18, QuestionId = 5 },
                new { MockExamId = 19, QuestionId = 5 },
                new { MockExamId = 20, QuestionId = 5 },
                new { MockExamId = 17, QuestionId = 11 },
                new { MockExamId = 18, QuestionId = 11 },
                new { MockExamId = 19, QuestionId = 11 },
                new { MockExamId = 20, QuestionId = 11 },
                new { MockExamId = 17, QuestionId = 12 },
                new { MockExamId = 18, QuestionId = 12 },
                new { MockExamId = 19, QuestionId = 12 },
                new { MockExamId = 20, QuestionId = 12 },
                new { MockExamId = 17, QuestionId = 13 },
                new { MockExamId = 18, QuestionId = 13 },
                new { MockExamId = 19, QuestionId = 13 },
                new { MockExamId = 20, QuestionId = 13 },
                new { MockExamId = 17, QuestionId = 14 },
                new { MockExamId = 18, QuestionId = 14 },
                new { MockExamId = 19, QuestionId = 14 },
                new { MockExamId = 20, QuestionId = 14 },
                new { MockExamId = 17, QuestionId = 15 },
                new { MockExamId = 18, QuestionId = 15 },
                new { MockExamId = 19, QuestionId = 15 },
                new { MockExamId = 20, QuestionId = 15 },
                new { MockExamId = 20, QuestionId = 16 },
                new { MockExamId = 20, QuestionId = 17 }
            );

            // 9. Answers
            modelBuilder.Entity<Answer>().HasData(
                new Answer { AnswerId = 5, AnswerText = "Được khách hàng, xã hội tôn trọng; được đồng nghiệp quý mến, giúp đỡ; được doanh nghiệp tin dùng và đóng góp nhiều cho xã hội.", IsCorrect = false, QuestionId = 1 },
                new Answer { AnswerId = 6, AnswerText = "Thu hút được khách hàng, góp phần quan trọng trong xây dựng thương hiệu, kinh doanh có hiệu quả cao.", IsCorrect = false, QuestionId = 1 },
                new Answer { AnswerId = 7, AnswerText = "Cả ý 1 và ý 2.", IsCorrect = true, QuestionId = 1 },
                new Answer { AnswerId = 9, AnswerText = "Thực hiện nghiêm chỉnh những nội dung hợp đồng giữa chủ phương tiện với chủ hàng trong việc vận chuyển và bảo quản hàng hóa trong quá trình vận chuyển; không chở hàng cấm, không xếp hàng quá trọng tải của xe, quá trọng tải cho phép của cầu, đường; khi vận chuyển hàng quá khổ, quá tải, hàng nguy hiểm, hàng siêu trường, siêu trọng phải có giấy phép.", IsCorrect = true, QuestionId = 2 },
                new Answer { AnswerId = 11, AnswerText = "Thực hiện nghiêm chỉnh những nội dung hợp đồng giữa chủ hàng với khách hàng trong việc vận chuyển và bảo quản hàng hóa trong quá trình vận chuyển; trong trường hợp cần thiết có thể xếp hàng quá trọng tải của xe, quá trọng tải cho phép của cầu theo yêu cầu của chủ hàng; khi vận chuyển hàng quá khổ, quá tải, hàng nguy hiểm, hàng siêu trường, siêu trọng phải được chủ hàng cho phép.", IsCorrect = false, QuestionId = 2 },
                new Answer { AnswerId = 12, AnswerText = "Phải yêu quý xe, quản lý và sử dụng xe tốt; bảo dưỡng xe đúng định kỳ; thực hành tiết kiệm vật tư, nhiên liệu; luôn tu dưỡng bản thân, có lối sống lành mạnh, tác phong làm việc công nghiệp.", IsCorrect = false, QuestionId = 3 },
                new Answer { AnswerId = 14, AnswerText = "Nắm vững các quy định của pháp luật, tự giác chấp hành pháp luật, lái xe an toàn; coi hành khách như người thân, là đối tác tin cậy; có ý thức tổ chức kỷ luật và xây dựng doanh nghiệp vững mạnh; có tinh thần hợp tác, tương trợ, giúp đỡ đồng nghiệp.", IsCorrect = false, QuestionId = 3 },
                new Answer { AnswerId = 15, AnswerText = "Cả ý 1 và ý 2.", IsCorrect = true, QuestionId = 3 },
                new Answer { AnswerId = 16, AnswerText = "Kiểm tra các điều kiện bảo đảm an toàn của xe sau khi khởi hành; có trách nhiệm lái xe thật nhanh khi chậm giờ của", IsCorrect = false, QuestionId = 4 },
                new Answer { AnswerId = 17, AnswerText = "Kiểm tra các điều kiện bảo đảm an toàn của xe trước khi khởi hành; có thái độ văn minh, lịch sự, hướng dẫn hành khách ngồi đúng nơi quy định; kiểm tra việc sắp xếp, chằng buộc hành lý, bảo đảm an toàn.", IsCorrect = false, QuestionId = 4 },
                new Answer { AnswerId = 18, AnswerText = "Cả ba biển.", IsCorrect = false, QuestionId = 11 },
                new Answer { AnswerId = 19, AnswerText = "Biển 2 và 3.", IsCorrect = false, QuestionId = 11 },
                new Answer { AnswerId = 20, AnswerText = "Biển 1 và 3.", IsCorrect = false, QuestionId = 11 },
                new Answer { AnswerId = 21, AnswerText = "Biển 1 và 2.", IsCorrect = true, QuestionId = 11 },
                new Answer { AnswerId = 22, AnswerText = "Phần mặt đường và lề đường.", IsCorrect = false, QuestionId = 12 },
                new Answer { AnswerId = 23, AnswerText = "Phần đường xe chạy.", IsCorrect = true, QuestionId = 12 },
                new Answer { AnswerId = 24, AnswerText = "Phần đường xe cơ giới.", IsCorrect = false, QuestionId = 12 },
                new Answer { AnswerId = 25, AnswerText = "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, sử dụng cho xe chạy.", IsCorrect = false, QuestionId = 13 },
                new Answer { AnswerId = 26, AnswerText = "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, có bề rộng đủ cho xe chạy an toàn.", IsCorrect = true, QuestionId = 13 },
                new Answer { AnswerId = 27, AnswerText = "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, có đủ bề rộng cho xe ô tô chạy an toàn.", IsCorrect = false, QuestionId = 13 },
                new Answer { AnswerId = 29, AnswerText = "Là khoảng trống có kích thước giới hạn về chiều cao, chiều rộng của đường, cầu, bến phà, hầm đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.", IsCorrect = true, QuestionId = 14 },
                new Answer { AnswerId = 32, AnswerText = "Là khoảng trống có kích thước giới hạn về chiều rộng của đường, cầu, bến phà, hầm trên đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.", IsCorrect = false, QuestionId = 14 },
                new Answer { AnswerId = 33, AnswerText = "Là khoảng trống có kích thước giới hạn về chiều cao của cầu, bến phà, hầm trên đường bộ để các xe đi qua được an toàn.", IsCorrect = false, QuestionId = 14 },
                new Answer { AnswerId = 34, AnswerText = "Là bộ phận của đường để ngăn cách không cho các loại xe vào những nơi không được phép.", IsCorrect = false, QuestionId = 15 },
                new Answer { AnswerId = 35, AnswerText = "Là bộ phận của đường để phân tách phần đường xe chạy và hành lang an toàn giao thông.", IsCorrect = false, QuestionId = 15 },
                new Answer { AnswerId = 36, AnswerText = "Là bộ phận của đường để phân chia mặt đường thành hai chiều xe chạy riêng biệt hoặc để phân chia phần đường của xe cơ giới và xe thô sơ.", IsCorrect = true, QuestionId = 15 },
                new Answer { AnswerId = 37, AnswerText = "Có biện pháp bảo vệ tính mạng, sức khỏe, tài sản của hành khách đi xe, giữ gìn trật tự, vệ sinh trong xe; đóng cửa lên xuống của xe trước và trong khi xe chạy.", IsCorrect = false, QuestionId = 4 },
                new Answer { AnswerId = 38, AnswerText = "Cả ý 2 và ý 3.", IsCorrect = true, QuestionId = 4 },
                new Answer { AnswerId = 39, AnswerText = "Là sự hiểu biết và chấp hành nghiêm chỉnh pháp luật về giao thông; là ý thức trách nhiệm với cộng đồng khi tham gia giao thông.", IsCorrect = false, QuestionId = 5 },
                new Answer { AnswerId = 40, AnswerText = "Là ứng xử có văn hóa, có tình yêu thương con người trong các tình huống không may xảy ra khi tham gia giao thông.", IsCorrect = false, QuestionId = 5 },
                new Answer { AnswerId = 41, AnswerText = "Cả ý 1 và ý 2.", IsCorrect = true, QuestionId = 5 },
                new Answer { AnswerId = 42, AnswerText = "Biển báo nguy hiểm.", IsCorrect = false, QuestionId = 16 },
                new Answer { AnswerId = 43, AnswerText = "Biển báo cấm.", IsCorrect = true, QuestionId = 16 },
                new Answer { AnswerId = 44, AnswerText = "Biển báo hiệu lệnh.", IsCorrect = false, QuestionId = 16 },
                new Answer { AnswerId = 45, AnswerText = "Biển báo chỉ dẫn.", IsCorrect = false, QuestionId = 16 },
                new Answer { AnswerId = 46, AnswerText = "Biển báo nguy hiểm.", IsCorrect = true, QuestionId = 17 },
                new Answer { AnswerId = 47, AnswerText = "Biển báo cấm.", IsCorrect = false, QuestionId = 17 },
                new Answer { AnswerId = 48, AnswerText = "Biển báo hiệu lệnh.", IsCorrect = false, QuestionId = 17 },
                new Answer { AnswerId = 49, AnswerText = "Biển báo chỉ dẫn.", IsCorrect = false, QuestionId = 17 }
            );


            // 7. MockExamAnswers
            modelBuilder.Entity<MockExamAnswer>().HasData(
                new MockExamAnswer { MockExamAnswerId = 1, IsCorrect = true, AnswerId = 5, MockExamId = 12, QuestionId = 1 },
                new MockExamAnswer { MockExamAnswerId = 2, IsCorrect = false, AnswerId = 11, MockExamId = 12, QuestionId = 2 },
                new MockExamAnswer { MockExamAnswerId = 3, IsCorrect = true, AnswerId = 5, MockExamId = 18, QuestionId = 1 },
                new MockExamAnswer { MockExamAnswerId = 4, IsCorrect = false, AnswerId = 12, MockExamId = 18, QuestionId = 2 }
            );

        }
    }
}
