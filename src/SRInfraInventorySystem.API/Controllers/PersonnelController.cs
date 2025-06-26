using Microsoft.AspNetCore.Mvc;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Application.Services;
using SRInfraInventorySystem.Shared.Results;

namespace SRInfraInventorySystem.API.Controllers
{
    /// <summary>
    /// Personel yönetimi için API endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/personnel")]
    [Tags("Personnel")]
    [Produces("application/json")]
    public class PersonnelController : ControllerBase
    {
        private readonly IPersonnelService _personnelService;

        public PersonnelController(IPersonnelService personnelService)
        {
            _personnelService = personnelService;
        }

        /// <summary>
        /// Tüm personeli listeler
        /// </summary>
        /// <returns>Sistemdeki tüm personelin listesi</returns>
        /// <response code="200">Personel listesi başarıyla getirildi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<List<PersonnelDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetAllPersonnel()
        {
            try
            {
                var result = await _personnelService.GetAllAsync();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// ID'ye göre personel detaylarını getirir
        /// </summary>
        /// <param name="id">Personel ID'si</param>
        /// <returns>Belirtilen ID'ye sahip personelin detayları</returns>
        /// <response code="200">Personel bulundu</response>
        /// <response code="404">Personel bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<PersonnelDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetPersonnelById(Guid id)
        {
            try
            {
                var result = await _personnelService.GetByIdAsync(id);
                return result.Success ? Ok(result) : NotFound(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Yeni personel kaydı oluşturur
        /// </summary>
        /// <param name="createPersonnelDto">Personel oluşturma bilgileri</param>
        /// <returns>Oluşturulan personelin detayları</returns>
        /// <response code="201">Personel başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<PersonnelDto>), 201)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> CreatePersonnel([FromBody] CreatePersonnelDto createPersonnelDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _personnelService.CreateAsync(createPersonnelDto);
                
                if (result.Success)
                {
                    return CreatedAtAction(
                        nameof(GetPersonnelById), 
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
        /// Mevcut personeli günceller
        /// </summary>
        /// <param name="id">Güncellenecek personelin ID'si</param>
        /// <param name="updatePersonnelDto">Güncelleme bilgileri</param>
        /// <returns>Güncellenen personelin detayları</returns>
        /// <response code="200">Personel başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="404">Personel bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<PersonnelDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> UpdatePersonnel(Guid id, [FromBody] CreatePersonnelDto updatePersonnelDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _personnelService.UpdateAsync(id, updatePersonnelDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Personeli siler
        /// </summary>
        /// <param name="id">Silinecek personelin ID'si</param>
        /// <returns>Silme işlemi sonucu</returns>
        /// <response code="204">Personel başarıyla silindi</response>
        /// <response code="400">Silme işlemi başarısız</response>
        /// <response code="404">Personel bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> DeletePersonnel(Guid id)
        {
            try
            {
                var result = await _personnelService.DeleteAsync(id);
                return result.Success ? NoContent() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }
    }
} 