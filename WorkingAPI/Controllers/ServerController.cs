using Microsoft.AspNetCore.Mvc;

namespace WorkingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServerController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllServers()
        {
            var servers = new[]
            {
                new { Id = 1, Name = "Web Server 1", Status = "Active", IP = "192.168.1.10" },
                new { Id = 2, Name = "Database Server", Status = "Active", IP = "192.168.1.20" },
                new { Id = 3, Name = "App Server", Status = "Maintenance", IP = "192.168.1.30" }
            };
            return Ok(new { Message = "Servers retrieved successfully", Count = servers.Length, Data = servers });
        }

        [HttpGet("{id}")]
        public IActionResult GetServer(int id)
        {
            var server = new { Id = id, Name = $"Server{id}", Status = "Active", CPU = "45%", Memory = "67%", LastUpdate = DateTime.UtcNow };
            return Ok(server);
        }

        [HttpPost]
        public IActionResult CreateServer([FromBody] dynamic serverData)
        {
            var newServer = new { Id = new Random().Next(1000, 9999), Name = "New Server", Status = "Created", CreatedAt = DateTime.UtcNow };
            return Created($"/api/servers/{newServer.Id}", newServer);
        }
    }
} 