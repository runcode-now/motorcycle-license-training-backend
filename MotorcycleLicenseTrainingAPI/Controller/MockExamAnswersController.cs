using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockExamAnswersController : ControllerBase
    {
        private readonly MotorcycleLicenseTrainingContext _context;

        public MockExamAnswersController(MotorcycleLicenseTrainingContext context)
        {
            _context = context;
        }

        // GET: api/MockExamAnswers/getByExam/{examId}
        [HttpGet("getByExam/{examId}")]
        [Authorize]
        public async Task<IActionResult> GetByExam(int examId)
        {
            try
            {
                // Kiểm tra bài thi có tồn tại không
                var mockExam = await _context.MockExams
                    .FirstOrDefaultAsync(me => me.MockExamId == examId);

                if (mockExam == null)
                {
                    return NotFound(new { message = $"Không tìm thấy bài thi với ID {examId}." });
                }

                // Lấy danh sách câu trả lời của người dùng
                var answers = await _context.MockExamAnswers
                    .Where(mea => mea.MockExamId == examId)
                    .Select(mea => new
                    {
                        mockExamAnswerId = mea.MockExamAnswerId,
                        mockExamId = mea.MockExamId,
                        questionId = mea.QuestionId,
                        answerId = mea.AnswerId,
                        isCorrect = mea.IsCorrect
                    })
                    .ToListAsync();

                if (!answers.Any())
                {
                    return NotFound(new { message = "Không tìm thấy câu trả lời nào cho bài thi này." });
                }

                return Ok(answers);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy câu trả lời: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy câu trả lời. Vui lòng thử lại sau." });
            }
        }
    }
}
