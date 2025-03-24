using AutoMapper;
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
        public async Task<IActionResult> Create([FromBody] PracticeHistoriesDto practiceHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var practiceHistoryMap = _mapper.Map<PracticeHistories>(practiceHistory);
            var createdPracticeHistory = await _practiceHistoryService.CreatePracticeHistoryAsync(practiceHistoryMap);

            return CreatedAtAction(nameof(GetById), new { id = createdPracticeHistory.PracticeHistoryId }, createdPracticeHistory);
        }

        // PUT: api/PracticeHistory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PracticeHistoriesDto practiceHistory)
        {
            var result = context.PracticeHistories.FirstOrDefaultAsync(x => x.PracticeHistoryId == id);

            if (result == null) { 
                return NotFound();
            }

            PracticeHistories updateObject = new PracticeHistories()
            {
                PracticeHistoryId = id,
                AnswerId = practiceHistory.AnswerId,
                IsCorrect = practiceHistory.IsCorrect,
                QuestionId = practiceHistory.QuestionId,
                UserId = practiceHistory.UserId
            };

            context.PracticeHistories.Add(updateObject);
            context.SaveChanges();

            return NoContent();
        }

        // GET: api/PracticeHistory/5
        [HttpGet("getPracticeHistory/{userId}/{questionId}")]
        public async Task<IActionResult> GetByUserIdAndQuestionId(string userId, int questionId)
        {

            var practiceHistory = await context.PracticeHistories
                .FirstOrDefaultAsync(x => x.UserId == userId && x.QuestionId == questionId);

            if (practiceHistory == null)
            {
                return NotFound();
            }

            return Ok(practiceHistory);
        }
    }
}
