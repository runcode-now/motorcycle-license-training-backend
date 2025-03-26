using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotorcycleLicenseTrainingAPI.Service.Interface;

namespace MotorcycleLicenseTrainingAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficSignController : ControllerBase
    {
        private readonly ITrafficSignervice _TrafficSignervice;

        public TrafficSignController(ITrafficSignervice TrafficSignervice)
        {
            _TrafficSignervice = TrafficSignervice;   
        }

        [HttpGet("getByCategory/{categoryId}")]
        [Authorize]
        public async Task<IActionResult> Get(int categoryId)
        {
            try
            {
                var trafficList = await _TrafficSignervice.GetTrafficSignByCategoryId(categoryId);
                return Ok(trafficList);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
