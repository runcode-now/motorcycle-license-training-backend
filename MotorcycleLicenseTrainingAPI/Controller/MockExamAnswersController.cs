using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockExamAnswerController : ControllerBase
    {
        private readonly MotorcycleLicenseTrainingContext _context;

        public MockExamAnswerController(MotorcycleLicenseTrainingContext context)
        {
            _context = context;
        }

        // GET: api/MockExamAnswer/getByExam/{examId}
        [HttpGet("getByExam/{examId}")]
        [Authorize]
        public async Task<IActionResult> GetByExam(int examId)
        {
            try
            {
                // Kiểm tra bài thi có tồn tại không
                var mockExam = await _context.MockExam
                    .FirstOrDefaultAsync(me => me.MockExamId == examId);

                if (mockExam == null)
                {
                    return NotFound(new { message = $"Không tìm thấy bài thi với ID {examId}." });
                }

                // Lấy danh sách câu trả lời của người dùng
                var Answer = await _context.MockExamAnswer
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

                if (!Answer.Any())
                {
                    return NotFound(new { message = "Không tìm thấy câu trả lời nào cho bài thi này." });
                }

                return Ok(Answer);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy câu trả lời: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy câu trả lời. Vui lòng thử lại sau." });
            }
        }

    }
}
