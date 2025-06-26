using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Erişim logu bilgilerini transfer etmek için kullanılan DTO.
    /// Güvenlik takibi için kullanılır.
    /// </summary>
    public class AccessLogDto
    {
        /// <summary>
        /// Erişim logu kaydının benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
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
        /// Erişim hakkında ek notlar
        /// </summary>
        public string Notes { get; set; }
        
        /// <summary>
        /// Erişim yapılan sunucunun ID'si
        /// </summary>
        public Guid? ServerId { get; set; }
        
        /// <summary>
        /// Erişim yapılan sunucunun adı
        /// </summary>
        public string ServerName { get; set; }
        
        /// <summary>
        /// Erişim yapılan uygulamanın ID'si
        /// </summary>
        public Guid? ApplicationId { get; set; }
        
        /// <summary>
        /// Erişim yapılan uygulamanın adı
        /// </summary>
        public string ApplicationName { get; set; }
        
        /// <summary>
        /// Erişim yapılan veritabanının ID'si
        /// </summary>
        public Guid? DatabaseId { get; set; }
        
        /// <summary>
        /// Erişim yapılan veritabanının adı
        /// </summary>
        public string DatabaseName { get; set; }
    }
} 