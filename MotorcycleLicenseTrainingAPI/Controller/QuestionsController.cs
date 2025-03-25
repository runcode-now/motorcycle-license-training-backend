using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private MotorcycleLicenseTrainingContext _context = new MotorcycleLicenseTrainingContext();
        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("getByCategory/{categoryId}")]
        [Authorize]
        public async Task<IActionResult> Get(int categoryId)
        {
            try
            {
                var questionList = await _questionService.GetQuestionByCategoryId(categoryId);
                return Ok(questionList);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        // GET: api/Questions/getById/{questionId}
        [HttpGet("getById/{questionId}")]
        [Authorize]
        public async Task<IActionResult> GetById(int questionId)
        {
            try
            {
                var question = await _context.Questions
                    .Where(q => q.QuestionId == questionId)
                    .Select(q => new
                    {
                        QuestionId = q.QuestionId,
                        QuestionContent = q.QuestionContent,
                        ImageUrl = q.ImageUrl,
                        Reason = q.Reason,
                        CategoryId = q.CategoryId,
                        Answers = q.Answers.Select(a => new
                        {
                            AnswerId = a.AnswerId,
                            AnswerText = a.AnswerText,
                            IsCorrect = a.IsCorrect,
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (question == null)
                {
                    return NotFound(new { message = $"Không tìm thấy câu hỏi với ID {questionId}." });
                }

                return Ok(question);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi lấy câu hỏi: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy câu hỏi. Vui lòng thử lại sau." });
            }
        }
    }
}
