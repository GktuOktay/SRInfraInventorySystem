using Microsoft.AspNetCore.Mvc;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Application.Services;
using SRInfraInventorySystem.Shared.Results;
using Swashbuckle.AspNetCore.Annotations;
using SRInfraInventorySystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SRInfraInventorySystem.API.Controllers
{
    /// <summary>
    /// Sunucu yönetimi için API endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/servers")]
    [Tags("Servers")]
    [Produces("application/json")]
    public class ServerController : ControllerBase
    {
        private readonly IServerService _serverService;

        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        /// <summary>
        /// Tüm sunucuları listeler
        /// </summary>
        /// <returns>Sistemdeki tüm sunucuların listesi</returns>
        /// <response code="200">Sunucular başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<List<ServerDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetAllServers()
        {
            try
            {
                var result = await _serverService.GetAllAsync();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// ID'ye göre sunucu detaylarını getirir
        /// </summary>
        /// <param name="id">Sunucu ID'si</param>
        /// <returns>Belirtilen ID'ye sahip sunucunun detayları</returns>
        /// <response code="200">Sunucu bulundu</response>
        /// <response code="404">Sunucu bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<ServerDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetServerById(Guid id)
        {
            try
            {
                var result = await _serverService.GetByIdAsync(id);
                return result.Success ? Ok(result) : NotFound(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Yeni sunucu kaydı oluşturur
        /// </summary>
        /// <param name="createServerDto">Sunucu oluşturma bilgileri</param>
        /// <returns>Oluşturulan sunucunun detayları</returns>
        /// <response code="201">Sunucu başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<ServerDto>), 201)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> CreateServer([FromBody] CreateServerDto createServerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _serverService.CreateAsync(createServerDto);
                
                if (result.Success)
                {
                    return CreatedAtAction(
                        nameof(GetServerById), 
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
        /// Mevcut sunucuyu günceller
        /// </summary>
        /// <param name="id">Güncellenecek sunucunun ID'si</param>
        /// <param name="updateServerDto">Güncelleme bilgileri</param>
        /// <returns>Güncellenen sunucunun detayları</returns>
        /// <response code="200">Sunucu başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="404">Sunucu bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<ServerDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> UpdateServer(Guid id, [FromBody] CreateServerDto updateServerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _serverService.UpdateAsync(id, updateServerDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Sunucuyu siler
        /// </summary>
        /// <param name="id">Silinecek sunucunun ID'si</param>
        /// <returns>Silme işlemi sonucu</returns>
        /// <response code="204">Sunucu başarıyla silindi</response>
        /// <response code="400">Silme işlemi başarısız</response>
        /// <response code="404">Sunucu bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> DeleteServer(Guid id)
        {
            try
            {
                var result = await _serverService.DeleteAsync(id);
                return result.Success ? NoContent() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Duruma göre sunucuları listeler
        /// </summary>
        /// <param name="status">Sunucu durumu (Active, Inactive, Maintenance)</param>
        /// <returns>Belirtilen durumda olan sunucuların listesi</returns>
        /// <response code="200">Sunucular başarıyla listelendi</response>
        /// <response code="400">Geçersiz durum parametresi</response>
        [HttpGet("status/{status}")]
        [SwaggerOperation(
            Summary = "Duruma göre sunucuları getir",
            Description = "Belirtilen durumda olan tüm sunucuları getirir. Durum: Active (Aktif), Inactive (Pasif), Maintenance (Bakımda)"
        )]
        [SwaggerResponse(200, "Başarılı", typeof(List<ServerDto>))]
        [SwaggerResponse(400, "Geçersiz parametre")]
        public async Task<IActionResult> GetByStatus(ServerStatus status)
        {
            var result = await _serverService.GetByStatusAsync(status);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// İşletim sistemine göre sunucuları listeler
        /// </summary>
        /// <param name="operatingSystem">İşletim sistemi adı (örn: Windows, Linux, Ubuntu)</param>
        /// <returns>Belirtilen işletim sistemine sahip sunucuların listesi</returns>
        /// <response code="200">Sunucular başarıyla listelendi</response>
        /// <response code="400">Hata durumunda mesaj döner</response>
        [HttpGet("os/{operatingSystem}")]
        [SwaggerOperation(
            Summary = "İşletim sistemine göre sunucuları getir",
            Description = "Belirtilen işletim sistemine sahip tüm sunucuları getirir. Örnek: Windows Server 2019, Ubuntu 20.04, CentOS 8"
        )]
        [SwaggerResponse(200, "Başarılı", typeof(List<ServerDto>))]
        [SwaggerResponse(400, "Hata")]
        public async Task<IActionResult> GetByOperatingSystem(string operatingSystem)
        {
            var result = await _serverService.GetByOperatingSystemAsync(operatingSystem);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
} 