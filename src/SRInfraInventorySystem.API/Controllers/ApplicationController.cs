using Microsoft.AspNetCore.Mvc;

namespace SRInfraInventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/applications")]
    public class ApplicationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllApplications()
        {
            return Ok(new { Message = "Applications endpoint working!", Applications = new[] { "App1", "App2" } });
        }

        [HttpGet("{id}")]
        public IActionResult GetApplication(int id)
        {
            return Ok(new { Id = id, Name = $"Application{id}", Status = "Running" });
        }
    }
} 