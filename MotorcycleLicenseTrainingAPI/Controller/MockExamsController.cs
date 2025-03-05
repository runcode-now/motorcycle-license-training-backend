using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleLicenseTrainingAPI.Model;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockExamsController : ControllerBase
    {
        private MotorcycleLicenseTrainingContext _context;

        public MockExamsController()
        {
            _context = new MotorcycleLicenseTrainingContext();
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
