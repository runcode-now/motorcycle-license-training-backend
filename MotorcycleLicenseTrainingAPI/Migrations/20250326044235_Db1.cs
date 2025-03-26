using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorcycleLicenseTrainingAPI.Migrations
{
    public partial class Db1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MockExam",
                columns: table => new
                {
                    MockExamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalScore = table.Column<int>(type: "int", nullable: true),
                    IsPassed = table.Column<bool>(type: "bit", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockExam", x => x.MockExamId);
                    table.ForeignKey(
                        name: "FK_MockExam_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticeHistory",
                columns: table => new
                {
                    PracticeHistoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerId = table.Column<int>(type: "int", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeHistory", x => x.PracticeHistoryId);
                    table.ForeignKey(
                        name: "FK_PracticeHistory_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFailing = table.Column<bool>(type: "bit", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrafficSign",
                columns: table => new
                {
                    TrafficSignId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrafficSignTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafficSignContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficSign", x => x.TrafficSignId);
                    table.ForeignKey(
                        name: "FK_TrafficSign_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MockExamAnswer",
                columns: table => new
                {
                    MockExamAnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    MockExamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockExamAnswer", x => x.MockExamAnswerId);
                    table.ForeignKey(
                        name: "FK_MockExamAnswer_MockExam_MockExamId",
                        column: x => x.MockExamId,
                        principalTable: "MockExam",
                        principalColumn: "MockExamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnswerText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answer_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MockExamQuestion",
                columns: table => new
                {
                    MockExamId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MockExamQuestion", x => new { x.MockExamId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_MockExamQuestion_MockExam_MockExamId",
                        column: x => x.MockExamId,
                        principalTable: "MockExam",
                        principalColumn: "MockExamId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MockExamQuestion_Question_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "77c46877-537a-4637-bafe-b76b21a2f56e", 0, "e32d454f-ad57-4df0-bb96-64eb8dd823aa", new DateTime(2025, 3, 25, 0, 4, 42, 607, DateTimeKind.Unspecified).AddTicks(9841), "demo11@gmail.com", false, "string", false, null, null, "DEMO11@GMAIL.COM", "AQAAAAEAACcQAAAAENcEUydoUtT/B7gQw9O0TMdMvVZ11kOL7BXsa428YPOG84sJRcW1fcOTuYyN+xzmFQ==", null, false, "291f971b-3b88-4a27-80d9-2a6e4710b719", false, "demo11@gmail.com" });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "CategoryName", "ImageUrl", "Type" },
                values: new object[,]
                {
                    { 1, "Biển báo cấm", null, "traffic_sign" },
                    { 2, "Biển hiệu lệnh", null, "traffic_sign" },
                    { 3, "Biển chỉ dẫn", null, "traffic_sign" },
                    { 4, "Biển báo nguy hiểm và cảnh báo", null, "traffic_sign" },
                    { 5, "Biển phụ", null, "traffic_sign" },
                    { 6, "Khái niệm và quy tắc", "https://png.pngtree.com/png-vector/20220925/ourmid/pngtree-a-man-explains-list-of-rule-guidelines-png-image_6212794.png", "theory" },
                    { 7, "Văn hóa và đạo đức lái xe", "https://trungtamanhngu.net/wp-content/uploads/2023/04/3.png", "theory" },
                    { 8, "Kỹ thuật lái xe", "https://lambanglaixe.vn/wp-content/uploads/2017/12/logo-745x800.png", "theory" },
                    { 9, "Biển báo đường bộ", "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-410.jpg?v=1600845714683", "theory" },
                    { 10, "Sa hình", "https://img.onthibanglaixe.net/blog/2023/5/kich-thuoc-vong-so-8-1688134433403.jpg", "theory" },
                    { 11, "Cảnh báo", "https://cdn-icons-png.flaticon.com/512/1410/1410040.png", "theory" }
                });

            migrationBuilder.InsertData(
                table: "MockExam",
                columns: new[] { "MockExamId", "ExamDate", "IsPassed", "Status", "TotalScore", "UserId" },
                values: new object[,]
                {
                    { 9, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "not_started", 90, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 10, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "not_started", 75, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 11, new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "not_started", 80, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 12, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "completed", 1, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 13, new DateTime(2025, 3, 24, 10, 0, 0, 0, DateTimeKind.Unspecified), true, "not_started", 85, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 14, new DateTime(2025, 3, 25, 14, 30, 0, 0, DateTimeKind.Unspecified), false, "not_started", 60, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 15, new DateTime(2025, 3, 24, 21, 41, 21, 56, DateTimeKind.Unspecified).AddTicks(2617), false, "not_started", -1, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 16, new DateTime(2025, 3, 24, 21, 44, 19, 492, DateTimeKind.Unspecified).AddTicks(969), false, "not_started", -1, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 17, new DateTime(2025, 3, 25, 6, 32, 8, 496, DateTimeKind.Unspecified).AddTicks(8371), false, "not_started", -1, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 18, new DateTime(2025, 3, 25, 6, 32, 8, 499, DateTimeKind.Unspecified).AddTicks(5312), false, "completed", 1, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 19, new DateTime(2025, 3, 25, 7, 46, 56, 492, DateTimeKind.Unspecified).AddTicks(5216), false, "not_started", -1, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 20, new DateTime(2025, 3, 25, 11, 39, 50, 158, DateTimeKind.Unspecified).AddTicks(9201), false, "not_started", -1, "77c46877-537a-4637-bafe-b76b21a2f56e" }
                });

            migrationBuilder.InsertData(
                table: "PracticeHistory",
                columns: new[] { "PracticeHistoryId", "AnswerId", "IsCorrect", "QuestionId", "UserId" },
                values: new object[,]
                {
                    { 1, 24, false, 12, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 2, 21, true, 11, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 3, 7, true, 1, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 4, 26, true, 13, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 5, 33, false, 14, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 6, 34, false, 15, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 7, 11, false, 2, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 8, 12, false, 3, "77c46877-537a-4637-bafe-b76b21a2f56e" },
                    { 9, 39, false, 5, "77c46877-537a-4637-bafe-b76b21a2f56e" }
                });

            migrationBuilder.InsertData(
                table: "Question",
                columns: new[] { "QuestionId", "CategoryId", "ImageUrl", "IsFailing", "QuestionContent", "Reason" },
                values: new object[,]
                {
                    { 1, 7, null, true, "Người hành nghề lái xe khi thực hiện tốt việc rèn luyện, nâng cao trách nhiệm, đạo đức nghề nghiệp sẽ thu được kết quả như thế nào?", null },
                    { 2, 7, null, false, "Người lái xe vận tải hàng hóa cần thực hiện những nhiệm vụ gì ghi ở dưới đây?", null },
                    { 3, 7, null, false, "Người lái xe kinh doanh vận tải cần thực hiện những công việc gì ghi ở dưới đây để thường xuyên rèn luyện nâng cao đạo đức nghề nghiệp?", "Tín hiệu vàng báo hiệu xe phải dừng trước vạch dừng; nếu đã vượt qua vạch thì được đi tiếp." },
                    { 4, 7, null, true, "Người lái xe và nhân viên phục vụ trên xe ô tô vận tải hành khách phải có những trách nhiệm gì theo quy định được ghi ở dưới đây?", "Theo quy định, lái xe sử dụng rượu bia sẽ bị phạt hành chính, tước bằng lái, nghiêm trọng có thể truy cứu trách nhiệm hình sự." },
                    { 5, 7, null, false, "Khái niệm về văn hóa giao thông được hiểu như thế nào là đúng?", "Tín hiệu vàng yêu cầu người lái xe dừng lại trước vạch dừng, trừ khi đã vượt qua vạch thì được tiếp tục đi." },
                    { 11, 9, "https://lythuyet.onthigplx.vn/images/onthigplx_vn__q306.webp", false, "Biển nào cấm ô tô tải?", "Không có" },
                    { 12, 6, null, false, "Phần của đường bộ được sử dụng cho các phương tiện giao thông qua lại là gì?", null },
                    { 13, 6, null, false, "“Làn đường” là gì?", null },
                    { 14, 6, null, false, "Khái niệm “Khổ giới hạn đường bộ” được hiểu như thế nào là đúng?", null },
                    { 15, 6, null, false, "Trong các khái niệm dưới đây, “dải phân cách” được hiểu như thế nào là đúng?", null },
                    { 16, 9, "https://thibanglaixe24h.net/wp-content/uploads/2020/08/bien-1.png", false, "Biển báo hiệu có dạng hình tròn, viền đỏ, nền trắng, trên nền có hình vẽ hoặc chữ số, chữ viết màu đen là loại biển gì dưới đây?", "Biển cấm: vòng tròn đỏ." },
                    { 17, 9, "https://thibanglaixe24h.net/wp-content/uploads/2020/08/bien-2.png", false, "Biển báo hiệu có dạng tam giác đều, viền đỏ, nền màu vàng, trên có hình vẽ màu đen là loại biển gì dưới đây?", null }
                });

            migrationBuilder.InsertData(
                table: "TrafficSign",
                columns: new[] { "TrafficSignId", "CategoryId", "ImageUrl", "TrafficSignContent", "TrafficSignTitle" },
                values: new object[,]
                {
                    { 1, 1, "https://lh4.googleusercontent.com/proxy/p-X43Y8E4jtza7YzFM6FbBDmQY2yMWKK6ZX5EvRToCqk1B9z79nPcmhw109LJJeRvf-iSkLQPhnUoLV1pu22cPNP_4o", "Để báo cấm các loại xe quay đầu (theo kiểu chữ U). Chiều mũi tên phù hợp với chiều cấm quay đầu xe. Biển số P124a có hiệu lực cấm các loại xe (có giới và thổ sơ) trừ các xe được ưu tiên theo quy định. Biển không có giá trị cấm rẽ trái để đi sang hướng đường khác.", "Biển số P.124a \"Cấm quay đầu xe\"" },
                    { 2, 1, "https://bizweb.dktcdn.net/100/352/036/files/bien-bao-105.jpg?v=1575706005214", "Để báo đường cấm các loại xe có giới và xe máy đi qua.", "Biển số P105 \"Cấm xe ô tô và xe máy\"" },
                    { 3, 1, "https://myanhsafety.com.vn/image/cache/catalog/bien-bao-giao-thong-duong-cam-p-101-1-500x500.jpg", "Để báo đường cấm các loại phương tiện đi lại cả hai hướng, trừ các xe được ưu tiên theo quy định.", "Biển số P101 \"Đường cấm\"" },
                    { 4, 1, "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/22578292-db52d-jpeg.jpg?v=1600507890367", "Để báo đường cấm các loại xe (có giới và thổ sơ) đi vào theo chiều đặt biển, trừ các xe được ưu tiên theo quy định. Người đi bộ được phép đi trên vỉa hè hoặc lề đường.", "Biển số P102 \"Cấm đi ngược chiều\"" },
                    { 6, 1, "https://giaothongmiennam.com/uploads/source/loaibienbao/103a.jpg", "Để báo đường cấm các loại xe có giới kể cả xe máy 3 bánh có thùng đi qua, trừ xe máy 2 bánh, xe gắn máy và các xe được ưu tiên theo quy định.", "Biển số P103a \"Cấm xe ô tô\"" },
                    { 7, 3, "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-401.jpg?v=1600847944553", "Biểu thị ưu tiên cho các phương tiện trên đường có đặt biển này được đi trước.", "Biển số I.401 \"Bắt đầu đường ưu tiên\"" },
                    { 8, 3, "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-402.jpg?v=1600846599747", "Biểu thị hết đoạn đường quy định là ưu tiên.", "Biển số I.402 \"Hết đoạn đường ưu tiên\"" },
                    { 9, 3, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-81QbPq3MXpSKC9-L1J8Sromeeo2jQRxzYw&s", "Chỉ lối rẽ vào đường cụt.", "Biển số I.405a \"Đường cụt\"" },
                    { 10, 3, "https://lh5.googleusercontent.com/proxy/R2QonNrldpzKmuP0W4wWKziXU4Yb5R08kL3XBFfzOuHwsmJm79K3pnfFZ8XqV-XIZPDV00QIrUcIIYPeEzqPbxITMUE", "Chỉ lối rẽ vào đường cụt.", "Biển số I.405b \"Đường cụt\"" }
                });

            migrationBuilder.InsertData(
                table: "TrafficSign",
                columns: new[] { "TrafficSignId", "CategoryId", "ImageUrl", "TrafficSignContent", "TrafficSignTitle" },
                values: new object[,]
                {
                    { 11, 2, "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-bao-dung-lai.jpg?v=1599812231573", "Để báo các xe (có giới và thổ sơ) dừng lại.", "Biển số R122 \"Dừng lại\"" },
                    { 12, 2, "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/5748397301-jpeg.jpg?v=1599903632117", "Khi đặt biển số R.301a ở trước nơi đường giao nhau thì hiệu lực tác dụng của biển là ở phạm vi khu vực nơi đường giao nhau phía sau biển tức là cấm xe rẽ phải hay rẽ trái. Nếu biển này đặt ở sau nơi đường giao nhau (bắt đầu vào đoạn đường phố) thì hiệu lực tác dụng của biển là từ vị trí đặt biển đến nơi đường giao nhau. Trong trường hợp này cấm rẽ trái và quay đầu trong vùng tác dụng của biển, chỉ cho phép rẽ phải vào cổng nhà hoặc ngõ phố có trên đoạn đường từ nơi đường giao nhau đặt biển đến nơi đường giao nhau tiếp theo.", "Biển số R.301a \"Hướng đi phải theo\"" },
                    { 13, 2, "https://bizweb.dktcdn.net/thumb/1024x1024/100/352/036/products/1831909bien-hieu-lenh-301e-jpeg.jpg?v=1599903903743", "Nhằm chỉ hướng cho phép xe đi ngang qua nơi đường giao nhau và ngăn chặn hướng đi ngược chiều trên đường phố với đường một chiều. Biển bắt buộc người tham gia giao thông chỉ được phép rẽ phải hoặc rẽ trái ở phạm vi nơi đường giao nhau trước mặt biển.", "Biển số R.301b \"Hướng đi phải theo\"" },
                    { 14, 4, "https://myanhsafety.com.vn/image/cache/catalog/bien-bao-nguy-hiem-201a-1-0x0.jpg", "Báo trước sắp đến một chỗ ngoặt nguy hiểm vòng bên trái.", "Biển số W.201a \"Chỗ ngoặt nguy hiểm\"" },
                    { 15, 4, "https://myanhsafety.com.vn/image/cache/catalog/bien-bao-nguy-hiem-201b-500x500.jpg", "Báo trước sắp đến một chỗ ngoặt nguy hiểm vòng bên phải.", "Biển số W.201b \"Chỗ ngoặt nguy hiểm\"" },
                    { 16, 4, "https://bizweb.dktcdn.net/thumb/grande/100/352/036/products/bien-202.jpg?v=1600248249870", "Báo trước sắp đến hai chỗ ngoặt ngược chiều nhau liên tiếp, ở gần nhau trong đó có ít nhất một chỗ ngoặt nguy hiểm.", "Biển số W.202a \"Nhiều chỗ ngoặt nguy hiểm liên tiếp\"" },
                    { 17, 5, "https://cdn-i.vtcnews.vn/resize/th/upload/2024/05/03/bien-bao-501-1-15032563.jpg", "Để thông báo chiều dài đoạn đường nguy hiểm hoặc cấm hoặc hạn chế bên dưới một số biển báo nguy hiểm, biển báo cấm hoặc hạn chế.", "Biển số S.501 \"Phạm vi tác dụng của biển\"" },
                    { 18, 5, "https://sathachmophong.com/wp-content/uploads/2022/12/Bien-so-S.502.webp", "Để thông báo khoảng cách thực tế từ vị trí đặt biển đến đối tượng báo hiệu ở phía trước. Con số trên biển ghi theo đơn vị mét (m) và lấy chân đến hàng chức mét.", "Biển số S.502 \"Khoảng cách đến đối tượng báo hiệu\"" },
                    { 19, 5, "https://lh5.googleusercontent.com/proxy/R5rT_cBAkJKAsB4nCjDP9xvGAOo1GZ9WLasPUDU2SxD2DaSSxVTWtyaAZ26X_dfqDTxk2d9UjP7eBpsW6PK9tpi88Y0Ra1EPpIUtOOksXw5wxCxM1hHcwxpFxGtsr1Gf-6K4RUQc8m1iKwPMDYw", "Để chỉ hướng tác dụng của biển là hướng vuông góc với chiều đi.", "Biển số S.503a \"Hướng tác dụng của biển\"" }
                });

            migrationBuilder.InsertData(
                table: "Answer",
                columns: new[] { "AnswerId", "AnswerText", "IsCorrect", "QuestionId" },
                values: new object[,]
                {
                    { 5, "Được khách hàng, xã hội tôn trọng; được đồng nghiệp quý mến, giúp đỡ; được doanh nghiệp tin dùng và đóng góp nhiều cho xã hội.", false, 1 },
                    { 6, "Thu hút được khách hàng, góp phần quan trọng trong xây dựng thương hiệu, kinh doanh có hiệu quả cao.", false, 1 },
                    { 7, "Cả ý 1 và ý 2.", true, 1 },
                    { 9, "Thực hiện nghiêm chỉnh những nội dung hợp đồng giữa chủ phương tiện với chủ hàng trong việc vận chuyển và bảo quản hàng hóa trong quá trình vận chuyển; không chở hàng cấm, không xếp hàng quá trọng tải của xe, quá trọng tải cho phép của cầu, đường; khi vận chuyển hàng quá khổ, quá tải, hàng nguy hiểm, hàng siêu trường, siêu trọng phải có giấy phép.", true, 2 },
                    { 11, "Thực hiện nghiêm chỉnh những nội dung hợp đồng giữa chủ hàng với khách hàng trong việc vận chuyển và bảo quản hàng hóa trong quá trình vận chuyển; trong trường hợp cần thiết có thể xếp hàng quá trọng tải của xe, quá trọng tải cho phép của cầu theo yêu cầu của chủ hàng; khi vận chuyển hàng quá khổ, quá tải, hàng nguy hiểm, hàng siêu trường, siêu trọng phải được chủ hàng cho phép.", false, 2 },
                    { 12, "Phải yêu quý xe, quản lý và sử dụng xe tốt; bảo dưỡng xe đúng định kỳ; thực hành tiết kiệm vật tư, nhiên liệu; luôn tu dưỡng bản thân, có lối sống lành mạnh, tác phong làm việc công nghiệp.", false, 3 },
                    { 14, "Nắm vững các quy định của pháp luật, tự giác chấp hành pháp luật, lái xe an toàn; coi hành khách như người thân, là đối tác tin cậy; có ý thức tổ chức kỷ luật và xây dựng doanh nghiệp vững mạnh; có tinh thần hợp tác, tương trợ, giúp đỡ đồng nghiệp.", false, 3 },
                    { 15, "Cả ý 1 và ý 2.", true, 3 },
                    { 16, "Kiểm tra các điều kiện bảo đảm an toàn của xe sau khi khởi hành; có trách nhiệm lái xe thật nhanh khi chậm giờ của", false, 4 },
                    { 17, "Kiểm tra các điều kiện bảo đảm an toàn của xe trước khi khởi hành; có thái độ văn minh, lịch sự, hướng dẫn hành khách ngồi đúng nơi quy định; kiểm tra việc sắp xếp, chằng buộc hành lý, bảo đảm an toàn.", false, 4 },
                    { 18, "Cả ba biển.", false, 11 },
                    { 19, "Biển 2 và 3.", false, 11 },
                    { 20, "Biển 1 và 3.", false, 11 },
                    { 21, "Biển 1 và 2.", true, 11 },
                    { 22, "Phần mặt đường và lề đường.", false, 12 },
                    { 23, "Phần đường xe chạy.", true, 12 },
                    { 24, "Phần đường xe cơ giới.", false, 12 },
                    { 25, "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, sử dụng cho xe chạy.", false, 13 },
                    { 26, "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, có bề rộng đủ cho xe chạy an toàn.", true, 13 },
                    { 27, "Là một phần của phần đường xe chạy được chia theo chiều dọc của đường, có đủ bề rộng cho xe ô tô chạy an toàn.", false, 13 },
                    { 29, "Là khoảng trống có kích thước giới hạn về chiều cao, chiều rộng của đường, cầu, bến phà, hầm đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.", true, 14 },
                    { 32, "Là khoảng trống có kích thước giới hạn về chiều rộng của đường, cầu, bến phà, hầm trên đường bộ để các xe kể cả hàng hóa xếp trên xe đi qua được an toàn.", false, 14 },
                    { 33, "Là khoảng trống có kích thước giới hạn về chiều cao của cầu, bến phà, hầm trên đường bộ để các xe đi qua được an toàn.", false, 14 },
                    { 34, "Là bộ phận của đường để ngăn cách không cho các loại xe vào những nơi không được phép.", false, 15 },
                    { 35, "Là bộ phận của đường để phân tách phần đường xe chạy và hành lang an toàn giao thông.", false, 15 },
                    { 36, "Là bộ phận của đường để phân chia mặt đường thành hai chiều xe chạy riêng biệt hoặc để phân chia phần đường của xe cơ giới và xe thô sơ.", true, 15 },
                    { 37, "Có biện pháp bảo vệ tính mạng, sức khỏe, tài sản của hành khách đi xe, giữ gìn trật tự, vệ sinh trong xe; đóng cửa lên xuống của xe trước và trong khi xe chạy.", false, 4 },
                    { 38, "Cả ý 2 và ý 3.", true, 4 },
                    { 39, "Là sự hiểu biết và chấp hành nghiêm chỉnh pháp luật về giao thông; là ý thức trách nhiệm với cộng đồng khi tham gia giao thông.", false, 5 },
                    { 40, "Là ứng xử có văn hóa, có tình yêu thương con người trong các tình huống không may xảy ra khi tham gia giao thông.", false, 5 },
                    { 41, "Cả ý 1 và ý 2.", true, 5 },
                    { 42, "Biển báo nguy hiểm.", false, 16 },
                    { 43, "Biển báo cấm.", true, 16 },
                    { 44, "Biển báo hiệu lệnh.", false, 16 },
                    { 45, "Biển báo chỉ dẫn.", false, 16 },
                    { 46, "Biển báo nguy hiểm.", true, 17 },
                    { 47, "Biển báo cấm.", false, 17 },
                    { 48, "Biển báo hiệu lệnh.", false, 17 },
                    { 49, "Biển báo chỉ dẫn.", false, 17 }
                });

            migrationBuilder.InsertData(
                table: "MockExamAnswer",
                columns: new[] { "MockExamAnswerId", "AnswerId", "IsCorrect", "MockExamId", "QuestionId" },
                values: new object[,]
                {
                    { 1, 5, true, 12, 1 },
                    { 2, 11, false, 12, 2 },
                    { 3, 5, true, 18, 1 }
                });

            migrationBuilder.InsertData(
                table: "MockExamAnswer",
                columns: new[] { "MockExamAnswerId", "AnswerId", "IsCorrect", "MockExamId", "QuestionId" },
                values: new object[] { 4, 12, false, 18, 2 });

            migrationBuilder.InsertData(
                table: "MockExamQuestion",
                columns: new[] { "MockExamId", "QuestionId" },
                values: new object[,]
                {
                    { 9, 1 },
                    { 10, 1 },
                    { 10, 2 },
                    { 12, 1 },
                    { 12, 2 },
                    { 12, 3 },
                    { 12, 4 },
                    { 12, 5 },
                    { 17, 1 },
                    { 17, 2 },
                    { 17, 3 },
                    { 17, 4 },
                    { 17, 5 },
                    { 17, 11 },
                    { 17, 12 },
                    { 17, 13 },
                    { 17, 14 },
                    { 17, 15 },
                    { 18, 1 },
                    { 18, 2 },
                    { 18, 3 },
                    { 18, 4 },
                    { 18, 5 },
                    { 18, 11 },
                    { 18, 12 },
                    { 18, 13 },
                    { 18, 14 },
                    { 18, 15 },
                    { 19, 1 },
                    { 19, 2 },
                    { 19, 3 },
                    { 19, 4 },
                    { 19, 5 },
                    { 19, 11 },
                    { 19, 12 },
                    { 19, 13 },
                    { 19, 14 },
                    { 19, 15 },
                    { 20, 1 },
                    { 20, 2 },
                    { 20, 3 }
                });

            migrationBuilder.InsertData(
                table: "MockExamQuestion",
                columns: new[] { "MockExamId", "QuestionId" },
                values: new object[,]
                {
                    { 20, 4 },
                    { 20, 5 },
                    { 20, 11 },
                    { 20, 12 },
                    { 20, 13 },
                    { 20, 14 },
                    { 20, 15 },
                    { 20, 16 },
                    { 20, 17 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MockExam_UserId",
                table: "MockExam",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MockExamAnswer_MockExamId",
                table: "MockExamAnswer",
                column: "MockExamId");

            migrationBuilder.CreateIndex(
                name: "IX_MockExamQuestion_QuestionId",
                table: "MockExamQuestion",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticeHistory_UserId",
                table: "PracticeHistory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_CategoryId",
                table: "Question",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TrafficSign_CategoryId",
                table: "TrafficSign",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "MockExamAnswer");

            migrationBuilder.DropTable(
                name: "MockExamQuestion");

            migrationBuilder.DropTable(
                name: "PracticeHistory");

            migrationBuilder.DropTable(
                name: "TrafficSign");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MockExam");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
