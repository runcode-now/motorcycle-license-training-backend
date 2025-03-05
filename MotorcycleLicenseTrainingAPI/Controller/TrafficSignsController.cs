using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficSignsController : ControllerBase
    {
        private readonly ITrafficSignService _trafficSignService;

        public TrafficSignsController(ITrafficSignService trafficsignService)
        {
            _trafficSignService = trafficsignService;   
        }

        [HttpGet("getByCategory/{categoryId}")]
        public async Task<IActionResult> Get(int categoryId)
        {
            try
            {
                var trafficList = await _trafficSignService.GetTrafficSignByCategoryId(categoryId);
                return Ok(trafficList);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
