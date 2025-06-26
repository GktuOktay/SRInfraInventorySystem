using Microsoft.AspNetCore.Mvc;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Application.Services;
using SRInfraInventorySystem.Shared.Results;

namespace SRInfraInventorySystem.API.Controllers
{
    /// <summary>
    /// Veritabanı yönetimi için API endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Tags("Databases")]
    [Produces("application/json")]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public DatabaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Tüm veritabanlarını listeler
        /// </summary>
        /// <returns>Sistemdeki tüm veritabanlarının listesi</returns>
        /// <response code="200">Veritabanları başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<List<DatabaseDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetAllDatabases()
        {
            try
            {
                var result = await _databaseService.GetAllAsync();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// ID'ye göre veritabanı detaylarını getirir
        /// </summary>
        /// <param name="id">Veritabanı ID'si</param>
        /// <returns>Belirtilen ID'ye sahip veritabanının detayları</returns>
        /// <response code="200">Veritabanı bulundu</response>
        /// <response code="404">Veritabanı bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<DatabaseDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetDatabaseById(Guid id)
        {
            try
            {
                var result = await _databaseService.GetByIdAsync(id);
                return result.Success ? Ok(result) : NotFound(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Yeni veritabanı kaydı oluşturur
        /// </summary>
        /// <param name="createDatabaseDto">Veritabanı oluşturma bilgileri</param>
        /// <returns>Oluşturulan veritabanının detayları</returns>
        /// <response code="201">Veritabanı başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<DatabaseDto>), 201)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> CreateDatabase([FromBody] CreateDatabaseDto createDatabaseDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _databaseService.CreateAsync(createDatabaseDto);
                
                if (result.Success)
                {
                    return CreatedAtAction(
                        nameof(GetDatabaseById), 
                        new { id = result.Data.Id }, 
                        result
                    );
                }
                
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Mevcut veritabanını günceller
        /// </summary>
        /// <param name="id">Güncellenecek veritabanının ID'si</param>
        /// <param name="updateDatabaseDto">Güncelleme bilgileri</param>
        /// <returns>Güncellenen veritabanının detayları</returns>
        /// <response code="200">Veritabanı başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="404">Veritabanı bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<DatabaseDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> UpdateDatabase(Guid id, [FromBody] CreateDatabaseDto updateDatabaseDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _databaseService.UpdateAsync(id, updateDatabaseDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Mevcut veritabanını kısmi olarak günceller (sadece gönderilen alanlar)
        /// </summary>
        /// <param name="id">Güncellenecek veritabanının ID'si</param>
        /// <param name="updateDatabaseDto">Güncellenecek alanlar</param>
        /// <returns>Güncellenen veritabanının detayları</returns>
        /// <response code="200">Veritabanı başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="404">Veritabanı bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<DatabaseDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> UpdateDatabasePartial(Guid id, [FromBody] UpdateDatabaseDto updateDatabaseDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _databaseService.UpdatePartialAsync(id, updateDatabaseDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Veritabanını siler
        /// </summary>
        /// <param name="id">Silinecek veritabanının ID'si</param>
        /// <returns>Silme işlemi sonucu</returns>
        /// <response code="204">Veritabanı başarıyla silindi</response>
        /// <response code="400">Silme işlemi başarısız</response>
        /// <response code="404">Veritabanı bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> DeleteDatabase(Guid id)
        {
            try
            {
                var result = await _databaseService.DeleteAsync(id);
                return result.Success ? NoContent() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Sunucuya göre veritabanlarını listeler
        /// </summary>
        /// <param name="serverId">Sunucu ID'si</param>
        /// <returns>Belirtilen sunucudaki veritabanlarının listesi</returns>
        /// <response code="200">Veritabanları başarıyla listelendi</response>
        /// <response code="400">Hata durumunda mesaj döner</response>
        [HttpGet("server/{serverId:guid}")]
        [ProducesResponseType(typeof(ApiResult<List<DatabaseDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<IActionResult> GetByServer(Guid serverId)
        {
            try
            {
                var result = await _databaseService.GetByServerAsync(serverId);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Veritabanı tipine göre listeler
        /// </summary>
        /// <param name="type">Veritabanı tipi (örn: SQL Server, MySQL, PostgreSQL)</param>
        /// <returns>Belirtilen tipteki veritabanlarının listesi</returns>
        /// <response code="200">Veritabanları başarıyla listelendi</response>
        /// <response code="400">Hata durumunda mesaj döner</response>
        [HttpGet("type/{type}")]
        [ProducesResponseType(typeof(ApiResult<List<DatabaseDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        public async Task<IActionResult> GetByType(string type)
        {
            try
            {
                var result = await _databaseService.GetByTypeAsync(type);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }
    }
} 