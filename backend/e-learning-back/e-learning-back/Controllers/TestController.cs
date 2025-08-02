using Microsoft.AspNetCore.Mvc;

namespace e_learning_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { message = "E-Learning API is working!", timestamp = DateTime.UtcNow });
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new { status = "Healthy", version = "1.0.0" });
        }
    }
} 