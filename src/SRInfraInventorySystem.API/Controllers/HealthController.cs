using Microsoft.AspNetCore.Mvc;

namespace SRInfraInventorySystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// API'nin sağlık durumunu kontrol eder
        /// </summary>
        /// <returns>API durumu</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { 
                Status = "Healthy", 
                Timestamp = DateTime.UtcNow,
                Message = "API is working!" 
            });
        }

        /// <summary>
        /// API'nin detaylı sağlık bilgilerini döner
        /// </summary>
        /// <returns>Detaylı API durumu</returns>
        [HttpGet("details")]
        public IActionResult GetDetails()
        {
            return Ok(new { 
                Status = "Healthy", 
                Message = "SR Infrastructure Inventory System API is running successfully!",
                Timestamp = DateTime.UtcNow,
                Version = "1.0.0",
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown",
                MachineName = Environment.MachineName,
                ProcessorCount = Environment.ProcessorCount,
                WorkingSet = Environment.WorkingSet
            });
        }
    }
} 