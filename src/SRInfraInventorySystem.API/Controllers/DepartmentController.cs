using Microsoft.AspNetCore.Mvc;
using SRInfraInventorySystem.Application.DTOs;
using SRInfraInventorySystem.Application.Services;
using SRInfraInventorySystem.Shared.Results;

namespace SRInfraInventorySystem.API.Controllers
{
    /// <summary>
    /// Departman yönetimi için API endpoint'leri
    /// </summary>
    [ApiController]
    [Route("api/departments")]
    [Tags("Departments")]
    [Produces("application/json")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Tüm departmanları listeler ve filtreleme yapar
        /// </summary>
        /// <param name="name">Departman adına göre filtreleme (kısmi eşleşme)</param>
        /// <param name="isActive">Aktif duruma göre filtreleme (true/false)</param>
        /// <param name="pageNumber">Sayfa numarası (varsayılan: 1)</param>
        /// <param name="pageSize">Sayfa boyutu (varsayılan: 10, maksimum: 100)</param>
        /// <returns>Filtrelenmiş departmanların listesi</returns>
        /// <response code="200">Departmanlar başarıyla listelendi</response>
        /// <response code="400">Geçersiz filtre parametreleri</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResult<List<DepartmentDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetAllDepartments(
            [FromQuery] string? name = null,
            [FromQuery] bool? isActive = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            try
            {
                // Parametre validasyonu
                if (pageNumber < 1)
                    return BadRequest(ApiResult<string>.ErrorResult("Sayfa numarası 1'den küçük olamaz"));

                if (pageSize < 1 || pageSize > 100)
                    return BadRequest(ApiResult<string>.ErrorResult("Sayfa boyutu 1-100 arasında olmalıdır"));

                // Filtreleme parametrelerini service'e geç
                var result = await _departmentService.GetFilteredDepartmentsAsync(
                    name, isActive, pageNumber, pageSize);
                
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Tüm departmanları basit liste olarak getirir (filtresiz)
        /// </summary>
        /// <returns>Sistemdeki tüm departmanların basit listesi</returns>
        /// <response code="200">Departmanlar başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("simple")]
        [ProducesResponseType(typeof(ApiResult<List<DepartmentDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetAllDepartmentsSimple()
        {
            try
            {
                var result = await _departmentService.GetAllAsync();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// ID'ye göre departman detaylarını getirir
        /// </summary>
        /// <param name="id">Departman ID'si</param>
        /// <returns>Belirtilen ID'ye sahip departmanın detayları</returns>
        /// <response code="200">Departman bulundu</response>
        /// <response code="404">Departman bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<DepartmentDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            try
            {
                var result = await _departmentService.GetByIdAsync(id);
                return result.Success ? Ok(result) : NotFound(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Yeni departman oluşturur
        /// </summary>
        /// <param name="createDepartmentDto">Departman oluşturma bilgileri</param>
        /// <returns>Oluşturulan departmanın temel bilgileri</returns>
        /// <response code="201">Departman başarıyla oluşturuldu</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResult<DepartmentResponseDto>), 201)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentDto createDepartmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _departmentService.CreateSimpleAsync(createDepartmentDto);
                
                if (result.Success)
                {
                    return CreatedAtAction(
                        nameof(GetDepartmentById), 
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
        /// Mevcut departmanı günceller
        /// </summary>
        /// <param name="id">Güncellenecek departmanın ID'si</param>
        /// <param name="updateDepartmentDto">Güncelleme bilgileri</param>
        /// <returns>Güncellenen departmanın detayları</returns>
        /// <response code="200">Departman başarıyla güncellendi</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="404">Departman bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResult<DepartmentDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] UpdateDepartmentDto updateDepartmentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _departmentService.UpdateAsync(id, updateDepartmentDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Ana departmanları getirir (parent departmanı olmayanlar)
        /// </summary>
        /// <returns>Ana departmanların listesi</returns>
        /// <response code="200">Ana departmanlar başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("root")]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<DepartmentDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetRootDepartments()
        {
            try
            {
                var result = await _departmentService.GetRootDepartmentsAsync();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Ana departmanları ve alt departmanlarını hiyerarşik yapıda select list için getirir
        /// </summary>
        /// <returns>Ana departmanların ve alt departmanlarının hiyerarşik listesi</returns>
        /// <response code="200">Ana departmanlar başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("root/select-list")]
        [ProducesResponseType(typeof(ApiResult<IEnumerable<DepartmentDropdownDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetRootDepartmentsForSelectList()
        {
            try
            {
                var result = await _departmentService.GetRootDepartmentsForSelectListAsync();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Departmana yönetici atar veya mevcut yöneticiyi kaldırır
        /// </summary>
        /// <param name="assignManagerDto">Yönetici atama bilgileri</param>
        /// <returns>Güncellenen departman bilgileri</returns>
        /// <response code="200">Yönetici başarıyla atandı/kaldırıldı</response>
        /// <response code="400">Geçersiz veri</response>
        /// <response code="404">Departman bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpPatch("assign-manager")]
        [ProducesResponseType(typeof(ApiResult<DepartmentDto>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> AssignManager([FromBody] AssignManagerDto assignManagerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ApiResult<string>.ErrorResult("Geçersiz model verisi"));

                var result = await _departmentService.AssignManagerAsync(assignManagerDto);
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Departmanı ve varsa tüm alt departmanlarını siler (Cascade Delete)
        /// </summary>
        /// <param name="id">Silinecek departmanın ID'si</param>
        /// <returns>Silme işlemi sonucu</returns>
        /// <remarks>
        /// Bu işlem ana departman silindiğinde tüm alt departmanları da siler.
        /// İşlem transaction ile yönetilir ve veri tutarlılığı sağlanır.
        /// </remarks>
        /// <response code="204">Departman (ve alt departmanları) başarıyla silindi</response>
        /// <response code="400">Silme işlemi başarısız</response>
        /// <response code="404">Departman bulunamadı</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiResult<string>), 400)]
        [ProducesResponseType(typeof(ApiResult<string>), 404)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            try
            {
                var result = await _departmentService.DeleteAsync(id);
                return result.Success ? NoContent() : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }

        /// <summary>
        /// Departmanları hiyerarşik ve alfabetik olarak döner
        /// </summary>
        /// <returns>Hiyerarşik departman listesi</returns>
        /// <response code="200">Departmanlar başarıyla listelendi</response>
        /// <response code="500">Sunucu hatası</response>
        [HttpGet("hierarchical")]
        [ProducesResponseType(typeof(ApiResult<List<HierarchicalDepartmentDto>>), 200)]
        [ProducesResponseType(typeof(ApiResult<string>), 500)]
        public async Task<IActionResult> GetHierarchicalDepartments()
        {
            try
            {
                var result = await _departmentService.GetAllHierarchicalAsync();
                return result.Success ? Ok(result) : BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResult<string>.ErrorResult($"Sunucu hatası: {ex.Message}"));
            }
        }
    }
} 