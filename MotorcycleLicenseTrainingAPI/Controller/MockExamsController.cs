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

                // Lấy ngẫu nhiên 25 câu hỏi
                var Question = await _context.Question
                    .OrderBy(q => Guid.NewGuid())
                    .Take(25)
                    .ToListAsync();

                //if (Question.Count < 25)
                //{
                //    return BadRequest(new { message = "Không đủ câu hỏi để tạo bài thi. Cần ít nhất 25 câu hỏi." });
                //}

                // Tạo bài thi mới
                var mockExam = new MockExam
                {
                    ExamDate = DateTime.Now,
                    TotalScore = -1,
                    UserId = createDto.UserId,
                    Status = "not_started"
                };

                // Thêm danh sách câu hỏi vào MockExam
                mockExam.Questions = Question;

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
                    .Include(me => me.Questions) // Lấy danh sách câu hỏi của bài thi
                    .FirstOrDefaultAsync(me => me.MockExamId == submission.ExamId);

                if (mockExam == null)
                {
                    return NotFound(new { message = $"Không tìm thấy bài thi với ID {submission.ExamId}." });
                }

                // Kiểm tra xem bài thi đã hoàn thành chưa
                if (mockExam.Status == "completed")
                {
                    return BadRequest(new { message = "Bài thi đã được nộp trước đó." });
                }

                // Kiểm tra userId
                if (mockExam.UserId != submission.UserId)
                {
                    return BadRequest(new { message = "Người dùng không có quyền nộp bài thi này." });
                }

                // Tính điểm và lưu lịch sử câu trả lời
                int score = 0;
                foreach (var userAnswer in submission.Answer)
                {
                    // Kiểm tra câu hỏi có thuộc bài thi không
                    var question = mockExam.Questions.FirstOrDefault(q => q.QuestionId == userAnswer.QuestionId);
                    if (question == null)
                    {
                        continue; // Bỏ qua nếu câu hỏi không thuộc bài thi
                    }

                    // Lấy đáp án đúng từ bảng Answer
                    var correctAnswer = await _context.Answer
                        .FirstOrDefaultAsync(a => a.QuestionId == userAnswer.QuestionId && a.IsCorrect == true);

                    bool isCorrect = false;
                    if (correctAnswer != null && userAnswer.AnswerId == correctAnswer.AnswerId)
                    {
                        score++;
                        isCorrect = true;
                    }

                    // Lưu câu trả lời của người dùng vào bảng MockExamAnswer
                    var mockExamAnswer = new MockExamAnswer
                    {
                        MockExamId = submission.ExamId,
                        QuestionId = userAnswer.QuestionId,
                        AnswerId = userAnswer.AnswerId,
                        IsCorrect = isCorrect
                    };
                    _context.MockExamAnswer.Add(mockExamAnswer);
                }

                // Cập nhật trạng thái bài thi
                mockExam.TotalScore = score;
                mockExam.IsPassed = score >= 1; // Ví dụ: Đậu nếu đạt 21/25
                mockExam.Status = "completed";

                // Lưu thay đổi vào database
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    score = score,
                    totalQuestion = mockExam.Questions.Count,
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
