using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Yeni erişim logu kaydı oluşturmak için kullanılan DTO.
    /// </summary>
    public class CreateAccessLogDto
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
        /// Erişim tarihi ve saati
        /// </summary>
        public DateTime AccessDate { get; set; }
        
        /// <summary>
        /// Erişim başlangıç zamanı
        /// </summary>
        public DateTime AccessTime { get; set; }
        
        /// <summary>
        /// Erişim bitiş zamanı
        /// </summary>
        public DateTime? ExitTime { get; set; }
        
        /// <summary>
        /// Kullanıcının IP adresi
        /// </summary>
        public string IpAddress { get; set; }
        
        /// <summary>
        /// Kullanıcının tarayıcı/user agent bilgisi
        /// </summary>
        public string UserAgent { get; set; }
        
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
    }
} 