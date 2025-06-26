using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Veritabanı bilgilerini transfer etmek için kullanılan DTO.
    /// Veritabanı detayları ve ilişkili kaynakları içerir.
    /// </summary>
    public class DatabaseDto
    {
        /// <summary>
        /// Veritabanının benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Veritabanının adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Veritabanının tipi
        /// </summary>
        public string Type { get; set; }
        
        /// <summary>
        /// Veritabanının versiyonu
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Veritabanından sorumlu kişi
        /// </summary>
        public string ResponsiblePerson { get; set; }
        
        /// <summary>
        /// Veritabanına erişim izinleri
        /// </summary>
        public string AccessPermissions { get; set; }
        
        /// <summary>
        /// Veritabanına bağlanmak için kullanılan araçlar
        /// </summary>
        public string ConnectionTools { get; set; }
        
        /// <summary>
        /// Veritabanı hakkında açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Veritabanının bulunduğu sunucunun ID'si
        /// </summary>
        public Guid ServerId { get; set; }
        
        /// <summary>
        /// Veritabanının bulunduğu sunucunun adı
        /// </summary>
        public string ServerName { get; set; }
        
        /// <summary>
        /// Veritabanına bağlı uygulamanın ID'si
        /// </summary>
        public Guid? ApplicationId { get; set; }
        
        /// <summary>
        /// Veritabanına bağlı uygulamanın adı
        /// </summary>
        public string ApplicationName { get; set; }
        
        /// <summary>
        /// Veritabanının oluşturulma tarihi
        /// </summary>
        public DateTime CreatedAt { get; set; }
        
        /// <summary>
        /// Veritabanının son güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        public List<AccessLogDto> AccessLogs { get; set; }
        
        public DatabaseDto()
        {
            AccessLogs = new List<AccessLogDto>();
        }
    }
} 