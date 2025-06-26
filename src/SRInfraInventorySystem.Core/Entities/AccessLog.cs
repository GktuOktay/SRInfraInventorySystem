using SRInfraInventorySystem.Core.Common;
using System;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Sistem kaynaklarına yapılan erişimleri loglayan entity.
    /// Güvenlik takibi ve audit trail için kullanılır.
    /// </summary>
    public class AccessLog : BaseEntity
    {
        /// <summary>
        /// Erişim yapan kullanıcının adı
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Erişim yöntemi (SSH, RDP, Web, API vb.)
        /// </summary>
        public string AccessMethod { get; set; }
        
        /// <summary>
        /// Kullanıcının sahip olduğu izinler
        /// </summary>
        public string Permissions { get; set; }
        
        /// <summary>
        /// Erişim yapılan IP adresi
        /// </summary>
        public string IpAddress { get; set; }
        
        /// <summary>
        /// Kullanıcının tarayıcı/istemci bilgisi
        /// </summary>
        public string UserAgent { get; set; }
        
        /// <summary>
        /// Erişim tarihi ve saati
        /// </summary>
        public DateTime AccessDate { get; set; }
        
        /// <summary>
        /// Erişim hakkında ek notlar
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Erişim yapılan sunucunun ID'si (opsiyonel)
        /// </summary>
        public Guid? ServerId { get; set; }
        
        /// <summary>
        /// Erişim yapılan uygulamanın ID'si (opsiyonel)
        /// </summary>
        public Guid? ApplicationId { get; set; }
        
        /// <summary>
        /// Erişim yapılan veritabanının ID'si (opsiyonel)
        /// </summary>
        public Guid? DatabaseId { get; set; }
        
        // Navigation Properties
        
        /// <summary>
        /// Erişim yapılan sunucu
        /// </summary>
        public Server Server { get; set; }
        
        /// <summary>
        /// Erişim yapılan uygulama
        /// </summary>
        public Application Application { get; set; }
        
        /// <summary>
        /// Erişim yapılan veritabanı
        /// </summary>
        public Database Database { get; set; }
    }
} 