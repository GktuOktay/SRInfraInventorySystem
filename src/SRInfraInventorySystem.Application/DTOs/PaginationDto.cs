using System.ComponentModel.DataAnnotations;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Sayfalama parametreleri için DTO.
    /// API'den gelen sayfalama isteklerini karşılar.
    /// </summary>
    public class PaginationDto
    {
        /// <summary>
        /// Sayfa numarası (varsayılan: 1)
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Sayfa numarası 1'den büyük olmalıdır.")]
        public int PageNumber { get; set; } = 1;
        
        /// <summary>
        /// Sayfa başına kayıt sayısı (varsayılan: 10, maksimum: 100)
        /// </summary>
        [Range(1, 100, ErrorMessage = "Sayfa boyutu 1 ile 100 arasında olmalıdır.")]
        public int PageSize { get; set; } = 10;
        
        /// <summary>
        /// Sıralama alanı (opsiyonel)
        /// </summary>
        public string? SortBy { get; set; }
        
        /// <summary>
        /// Sıralama yönü (asc/desc, varsayılan: asc)
        /// </summary>
        public string SortDirection { get; set; } = "asc";
        
        /// <summary>
        /// Arama terimi (opsiyonel)
        /// </summary>
        public string? SearchTerm { get; set; }
        
        /// <summary>
        /// Filtreleme parametreleri (JSON formatında, opsiyonel)
        /// </summary>
        public string? Filters { get; set; }
    }
} 