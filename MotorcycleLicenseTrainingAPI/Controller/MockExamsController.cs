using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockExamController : ControllerBase
    {
        private MotorcycleLicenseTrainingContext _context = new MotorcycleLicenseTrainingContext();

        public MockExamController()
        {
        }

        [HttpGet("getByUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                List<MockExam> MockExamList = _context.MockExam.Where(x => x.UserId == userId).ToList();

                if (MockExamList.Count == 0)
                {
                    return NotFound();
                }

                return Ok(MockExamList);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // POST: api/MockExam/create
        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] MockExamDto createDto)
        {
            try
            {
                if (createDto == null || string.IsNullOrEmpty(createDto.UserId))
                {
                    return BadRequest(new { message = "Dữ liệu không hợp lệ. Vui lòng cung cấp userId." });
                }

                // Thống kê số lần trả lời sai từ MockExamAnswer
                var wrongAnswersStats = await _context.MockExamAnswer
                    .Where(a => !a.IsCorrect) // Lọc các câu trả lời sai
                    .GroupBy(a => a.QuestionId)
                    .Select(g => new
                    {
                        QuestionId = g.Key,
                        WrongCount = g.Count() // Đếm số lần sai
                    })
                    .OrderByDescending(x => x.WrongCount) // Sắp xếp theo số lần sai giảm dần
                    .ToListAsync();

                // Lấy danh sách tất cả câu hỏi từ bảng Question
                var allQuestions = await _context.Question.ToListAsync();

                // Nếu không có dữ liệu thống kê sai, chọn ngẫu nhiên từ tất cả câu hỏi
                if (!wrongAnswersStats.Any())
                {
                    var randomQuestions = allQuestions
                        .OrderBy(q => Guid.NewGuid())
                        .Take(3)
                        .ToList();

                    if (randomQuestions.Count < 3)
                    {
                        return BadRequest(new { message = "Không đủ câu hỏi để tạo bài thi. Cần ít nhất 3 câu hỏi." });
                    }

                    var mock = new MockExam
                    {
                        ExamDate = DateTime.Now,
                        TotalScore = -1,
                        UserId = createDto.UserId,
                        Status = "not_started",
                        Questions = randomQuestions
                    };

                    _context.MockExam.Add(mock);
                    await _context.SaveChangesAsync();
                    return Ok();
                }

                // Chia danh sách thành 3 nhóm: sai nhiều, sai trung bình, sai ít
                int totalCount = wrongAnswersStats.Count;
                if (totalCount < 3)
                {
                    return BadRequest(new { message = "Không đủ câu hỏi sai để chia nhóm. Cần ít nhất 3 câu hỏi." });
                }

                int groupSize = totalCount / 3;
                var highWrongGroup = wrongAnswersStats.Take(groupSize).ToList(); // Sai nhiều
                var mediumWrongGroup = wrongAnswersStats.Skip(groupSize).Take(groupSize).ToList(); // Sai trung bình
                var lowWrongGroup = wrongAnswersStats.Skip(2 * groupSize).ToList(); // Sai ít

                // Lấy ngẫu nhiên 1 câu từ mỗi nhóm
                var selectedQuestions = new List<Question>();
                Random rand = new Random();

                // Sai nhiều (High Wrong)
                var highWrongQuestions = highWrongGroup
                    .OrderBy(x => rand.Next())
                    .Take(1)
                    .Select(x => allQuestions.FirstOrDefault(q => q.QuestionId == x.QuestionId))
                    .Where(q => q != null)
                    .ToList();
                selectedQuestions.AddRange(highWrongQuestions);

                // Sai trung bình (Medium Wrong)
                var mediumWrongQuestions = mediumWrongGroup
                    .OrderBy(x => rand.Next())
                    .Take(1)
                    .Select(x => allQuestions.FirstOrDefault(q => q.QuestionId == x.QuestionId))
                    .Where(q => q != null && !selectedQuestions.Contains(q))
                    .ToList();
                selectedQuestions.AddRange(mediumWrongQuestions);

                // Sai ít (Low Wrong)
                var lowWrongQuestions = lowWrongGroup
                    .OrderBy(x => rand.Next())
                    .Take(1)
                    .Select(x => allQuestions.FirstOrDefault(q => q.QuestionId == x.QuestionId))
                    .Where(q => q != null && !selectedQuestions.Contains(q))
                    .ToList();
                selectedQuestions.AddRange(lowWrongQuestions);

                // Nếu không đủ 3 câu hỏi từ các nhóm, bổ sung ngẫu nhiên từ tất cả câu hỏi
                while (selectedQuestions.Count < 3)
                {
                    var remainingQuestions = allQuestions
                        .Where(q => !selectedQuestions.Contains(q))
                        .ToList();
                    if (!remainingQuestions.Any())
                    {
                        return BadRequest(new { message = "Không đủ câu hỏi để tạo bài thi. Cần ít nhất 3 câu hỏi." });
                    }
                    var randomQuestion = remainingQuestions[rand.Next(remainingQuestions.Count)];
                    selectedQuestions.Add(randomQuestion);
                }

                // Tạo bài thi mới
                var mockExam = new MockExam
                {
                    ExamDate = DateTime.Now,
                    TotalScore = -1,
                    UserId = createDto.UserId,
                    Status = "not_started",
                    Questions = selectedQuestions.Take(3).ToList() // Đảm bảo chỉ lấy 3 câu
                };

                _context.MockExam.Add(mockExam);
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo bài thi mới: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi tạo bài thi mới. Vui lòng thử lại sau." });
            }
        }

        // GET: api/MockExam/{examId}
        [HttpGet("{examId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int examId)
        {
            try
            {
                var mockExam = await _context.MockExam.Include(x => x.Questions).FirstOrDefaultAsync(me => me.MockExamId == examId);

                if (mockExam == null)
                {
                    return NotFound(new { message = $"Không tìm thấy bài thi với ID {examId}." });
                }

                return Ok(mockExam);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy bài thi: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy bài thi. Vui lòng thử lại sau." });
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _context.MockExam.Remove(_context.MockExam.FirstOrDefault(x => x.MockExamId == id));
            _context.SaveChanges();
            return Ok("Get all Category");
        }

        [HttpPost("retake/{mockExamId}")]
        [Authorize]
        public async Task<IActionResult> RetakeExam(int mockExamId)
        {
            try
            {
                // Lấy bài thi cũ
                var mockExam = await _context.MockExam
                    .Include(e => e.Questions) // Lấy danh sách câu hỏi
                    .FirstOrDefaultAsync(e => e.MockExamId == mockExamId);

                if (mockExam == null)
                {
                    return NotFound("Bài thi không tồn tại.");
                }

                // Đặt lại trạng thái bài thi
                mockExam.TotalScore = -1; // Đặt lại điểm
                mockExam.IsPassed = false; // Chưa hoàn thành
                mockExam.Status = "not_started"; // Cho phép làm lại
                mockExam.ExamDate = null; // Đặt lại thời gian (tùy chọn)

                // Xóa tất cả câu trả lời cũ trong MockExamAnswer
                var existingAnswers = await _context.MockExamAnswer
                    .Where(ma => ma.MockExamId == mockExamId)
                    .ToListAsync();
                _context.MockExamAnswer.RemoveRange(existingAnswers);

                // Lưu thay đổi
                await _context.SaveChangesAsync();

                // Trả về thông tin bài thi đã được reset
                return Ok(mockExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        // POST: api/MockExam/submit
        [HttpPost("submit")]
        [Authorize]
        public async Task<IActionResult> Submit([FromBody] MockExamubmissionDto submission)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (submission == null || submission.Answer == null || !submission.Answer.Any())
                {
                    return BadRequest(new { message = "Dữ liệu không hợp lệ. Vui lòng cung cấp danh sách câu trả lời." });
                }

                // Kiểm tra bài thi có tồn tại không
                var mockExam = await _context.MockExam
                    .Include(me => me.Questions)
                    .FirstOrDefaultAsync(me => me.MockExamId == submission.ExamId);

                if (mockExam == null)
                {
                    return NotFound(new { message = $"Không tìm thấy bài thi với ID {submission.ExamId}." });
                }

                // Kiểm tra userId
                if (mockExam.UserId != submission.UserId)
                {
                    return BadRequest(new { message = "Người dùng không có quyền nộp bài thi này." });
                }

                // Tính điểm và xử lý câu trả lời
                int score = 0;
                foreach (var userAnswer in submission.Answer)
                {
                    var question = mockExam.Questions.FirstOrDefault(q => q.QuestionId == userAnswer.QuestionId);
                    if (question == null)
                    {
                        continue;
                    }

                    var correctAnswer = await _context.Answer
                        .FirstOrDefaultAsync(a => a.QuestionId == userAnswer.QuestionId && a.IsCorrect == true);

                    bool isCorrect = false;
                    if (correctAnswer != null && userAnswer.AnswerId == correctAnswer.AnswerId)
                    {
                        score++;
                        isCorrect = true;
                    }

                    // Kiểm tra và cập nhật hoặc thêm mới MockExamAnswer
                    var existingAnswer = await _context.MockExamAnswer
                        .FirstOrDefaultAsync(ma => ma.MockExamId == submission.ExamId && ma.QuestionId == userAnswer.QuestionId);

                    if (existingAnswer != null)
                    {
                        existingAnswer.AnswerId = userAnswer.AnswerId;
                        existingAnswer.IsCorrect = isCorrect;
                        _context.MockExamAnswer.Update(existingAnswer);
                    }
                    else
                    {
                        var mockExamAnswer = new MockExamAnswer
                        {
                            MockExamId = submission.ExamId,
                            QuestionId = userAnswer.QuestionId,
                            AnswerId = userAnswer.AnswerId,
                            IsCorrect = isCorrect
                        };
                        _context.MockExamAnswer.Add(mockExamAnswer);
                    }
                }

                // Cập nhật trạng thái bài thi (ghi đè kết quả cũ)
                mockExam.TotalScore = score;
                mockExam.IsPassed = score >= 3; // Đậu nếu đạt 21/25
                mockExam.Status = "completed";
                mockExam.ExamDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    score = score,
                    totalQuestions = mockExam.Questions.Count,
                    isPassed = mockExam.IsPassed
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi nộp bài thi: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi nộp bài thi. Vui lòng thử lại sau." });
            }
        }

    }


}
