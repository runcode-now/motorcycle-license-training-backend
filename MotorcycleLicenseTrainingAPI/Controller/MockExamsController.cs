using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockExamsController : ControllerBase
    {
        private MotorcycleLicenseTrainingContext _context = new MotorcycleLicenseTrainingContext();

        public MockExamsController()
        {
        }

        [HttpGet("getByUser/{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            try
            {
                List<MockExams> mockExamsList = _context.MockExams.Where(x => x.UserId == userId).ToList();
                
                if (mockExamsList.Count == 0)
                {
                    return NotFound();
                }

                return Ok(mockExamsList);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // POST: api/MockExams/create
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] MockExamsDto createDto)
        {
            try
            {
                if (createDto == null || string.IsNullOrEmpty(createDto.UserId))
                {
                    return BadRequest(new { message = "Dữ liệu không hợp lệ. Vui lòng cung cấp userId." });
                }

                // Tạo bài thi mới với 25 câu hỏi ngẫu nhiên
                var questions = await _context.Questions
                    .OrderBy(q => Guid.NewGuid()) // Chọn ngẫu nhiên
                    .Take(25) // Lấy 25 câu hỏi
                    .Select(q => q.QuestionId)
                    .ToListAsync();

                if (questions.Count < 25)
                {
                    return BadRequest(new { message = "Không đủ câu hỏi để tạo bài thi. Cần ít nhất 25 câu hỏi." });
                }

                var mockExam = new MockExams
                {
                    ExamDate = DateTime.Now,
                    TotalScore = -1, 
                    UserId = createDto.UserId,
                    Status = "not_started",
                };

                _context.MockExams.Add(mockExam);
                await _context.SaveChangesAsync();

                // Lưu danh sách câu hỏi vào bảng trung gian (MockExamQuestions)
                var examQuestions = questions.Select(qId => new MockExamQuestions
                                                        {
                                                            ExamId = mockExam.ExamId,
                                                            QuestionId = qId
                                                        }).ToList();

                _context.MockExamQuestions.AddRange(examQuestions);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetByUser), new { userId = mockExam.UserId }, new
                {
                    examId = mockExam.ExamId,
                    userId = mockExam.UserId,
                    status = mockExam.Status,
                    totalTime = mockExam.TotalTime
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tạo bài thi mới: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi tạo bài thi mới. Vui lòng thử lại sau." });
            }
        }

        // POST: api/MockExams/start/{examId}
        [HttpPost("start/{examId}")]
        public async Task<IActionResult> Start(int examId)
        {
            try
            {
                var mockExam = await _context.MockExams.FindAsync(examId);
                if (mockExam == null)
                {
                    return NotFound(new { message = $"Không tìm thấy bài thi với ID {examId}." });
                }

                // Đảm bảo bài thi ở trạng thái not_started
                if (mockExam.Status != "not_started")
                {
                    return BadRequest(new { message = "Bài thi đã được hoàn thành hoặc không ở trạng thái bắt đầu." });
                }

                return Ok(new
                {
                    examId = mockExam.ExamId,
                    userId = mockExam.UserId,
                    status = mockExam.Status,
                    totalTime = mockExam.TotalTime
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi bắt đầu bài thi: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi bắt đầu bài thi. Vui lòng thử lại sau." });
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _context.MockExams.Remove(_context.MockExams.FirstOrDefault(x => x.MockExamId == id));
            _context.SaveChanges();
            return Ok("Get all categories");
        }
    }
}
