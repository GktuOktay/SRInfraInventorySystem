using System.ComponentModel.DataAnnotations;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Veritabanı güncelleme DTO'su - sadece güncellenecek alanları içerir
    /// </summary>
    public class UpdateDatabaseDto
    {
        [StringLength(100, ErrorMessage = "Veritabanı adı en fazla 100 karakter olabilir")]
        public string? Name { get; set; }

        [StringLength(50, ErrorMessage = "Veritabanı tipi en fazla 50 karakter olabilir")]
        public string? Type { get; set; }

        [StringLength(20, ErrorMessage = "Versiyon en fazla 20 karakter olabilir")]
        public string? Version { get; set; }

        [StringLength(500, ErrorMessage = "Bağlantı dizesi en fazla 500 karakter olabilir")]
        public string? ConnectionString { get; set; }

        [StringLength(100, ErrorMessage = "Sorumlu kişi adı en fazla 100 karakter olabilir")]
        public string? ResponsiblePerson { get; set; }

        [StringLength(200, ErrorMessage = "Erişim izinleri en fazla 200 karakter olabilir")]
        public string? AccessPermissions { get; set; }

        [StringLength(100, ErrorMessage = "Bağlantı araçları en fazla 100 karakter olabilir")]
        public string? ConnectionTools { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Description { get; set; }
    }
} 