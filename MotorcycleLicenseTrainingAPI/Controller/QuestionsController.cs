using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("getByCategory/{categoryId}")]
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
    }
}
