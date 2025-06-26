using Microsoft.AspNetCore.Mvc;

namespace WorkingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllDatabases()
        {
            var databases = new[]
            {
                new { Id = 1, Name = "ProductionDB", Type = "SQL Server", Status = "Online", Size = "15GB" },
                new { Id = 2, Name = "DevDB", Type = "MySQL", Status = "Online", Size = "2GB" },
                new { Id = 3, Name = "AnalyticsDB", Type = "PostgreSQL", Status = "Maintenance", Size = "50GB" }
            };
            return Ok(new { Message = "Databases retrieved successfully", Count = databases.Length, Data = databases });
        }

        [HttpGet("{id}")]
        public IActionResult GetDatabase(int id)
        {
            var db = new { Id = id, Name = $"Database{id}", Type = "SQL Server", Status = "Online", Connections = 25, LastBackup = DateTime.UtcNow.AddHours(-6) };
            return Ok(db);
        }

        [HttpPost]
        public IActionResult CreateDatabase([FromBody] dynamic dbData)
        {
            var newDb = new { Id = new Random().Next(1000, 9999), Name = "New Database", Type = "SQL Server", Status = "Created", CreatedAt = DateTime.UtcNow };
            return Created($"/api/databases/{newDb.Id}", newDb);
        }
    }
} 