using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Yeni veritabanı oluşturmak için kullanılan DTO.
    /// </summary>
    public class CreateDatabaseDto
    {
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
        /// Veritabanına bağlı uygulamanın ID'si (opsiyonel)
        /// </summary>
        public Guid? ApplicationId { get; set; }
    }
} 