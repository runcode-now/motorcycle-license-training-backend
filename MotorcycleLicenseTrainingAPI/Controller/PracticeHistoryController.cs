using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotorcycleLicenseTrainingAPI.DTO;
using MotorcycleLicenseTrainingAPI.Model;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PracticeHistoryController : ControllerBase
    {
        private readonly IPracticeHistoryService _practiceHistoryService;
        private readonly IMapper _mapper;
        MotorcycleLicenseTrainingContext context = new MotorcycleLicenseTrainingContext();
        public PracticeHistoryController(IPracticeHistoryService practiceHistoryService, IMapper mapper)
        {
            _practiceHistoryService = practiceHistoryService;
            _mapper = mapper;
        }

        // POST: api/PracticeHistory
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] PracticeHistoryDto practiceHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var practiceHistoryMap = _mapper.Map<PracticeHistory>(practiceHistory);
            var createdPracticeHistory = await _practiceHistoryService.CreatePracticeHistoryAsync(practiceHistoryMap);

            return Ok(createdPracticeHistory);
        }

        // PUT: api/PracticeHistory/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PracticeHistoryDto practiceHistory)
        {
            PracticeHistory result = await context.PracticeHistory.FirstOrDefaultAsync(x => x.PracticeHistoryId == id);

            if (result == null)
            {
                return NotFound();
            }

            result.AnswerId = practiceHistory.AnswerId;
            result.IsCorrect = practiceHistory.IsCorrect;
            result.QuestionId = practiceHistory.QuestionId;
            result.UserId = practiceHistory.UserId;

            context.PracticeHistory.Update(result);
            await context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/PracticeHistory/5
        [Authorize]
        [HttpGet("getPracticeHistory/{userId}/{questionId}")]
        public async Task<IActionResult> GetByUserIdAndQuestionId(string userId, int questionId)
        {

            var practiceHistory = await context.PracticeHistory
                .FirstOrDefaultAsync(x => x.UserId == userId && x.QuestionId == questionId);

            if (practiceHistory == null)
            {
                return NotFound();
            }

            return Ok(practiceHistory);
        }

        // GET: api/PracticeHistory/getByUser/{userId}
        [HttpGet("getByUser/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var PracticeHistory = await context.PracticeHistory
                    .Where(ph => ph.UserId == userId)
                    .ToListAsync();

                if (PracticeHistory == null || !PracticeHistory.Any())
                {
                    return NotFound(new { message = "Không tìm thấy lịch sử trả lời cho người dùng này." });
                }

                return Ok(PracticeHistory);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                Console.WriteLine($"Lỗi khi lấy lịch sử trả lời: {ex.Message}");
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy lịch sử trả lời. Vui lòng thử lại sau." });
            }
        }
    }
}
