using Microsoft.AspNetCore.Mvc;

namespace WorkingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllApplications()
        {
            var applications = new[]
            {
                new { Id = 1, Name = "Web Portal", Version = "2.1.0", Status = "Running", Port = 8080 },
                new { Id = 2, Name = "API Gateway", Version = "1.5.2", Status = "Running", Port = 9000 },
                new { Id = 3, Name = "Background Service", Version = "3.0.1", Status = "Stopped", Port = 0 }
            };
            return Ok(new { Message = "Applications retrieved successfully", Count = applications.Length, Data = applications });
        }

        [HttpGet("{id}")]
        public IActionResult GetApplication(int id)
        {
            var app = new { Id = id, Name = $"Application{id}", Version = "1.0.0", Status = "Running", LastDeployment = DateTime.UtcNow.AddDays(-5) };
            return Ok(app);
        }

        [HttpPost]
        public IActionResult CreateApplication([FromBody] dynamic appData)
        {
            var newApp = new { Id = new Random().Next(1000, 9999), Name = "New Application", Version = "1.0.0", Status = "Deployed", CreatedAt = DateTime.UtcNow };
            return Created($"/api/applications/{newApp.Id}", newApp);
        }
    }
} 