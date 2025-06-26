using SRInfraInventorySystem.Core.Enums;
using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Uygulama bilgilerini transfer etmek için kullanılan DTO.
    /// Uygulama detayları ve sunucu ilişkisini içerir.
    /// </summary>
    public class ApplicationDto
    {
        /// <summary>
        /// Uygulamanın benzersiz tanımlayıcısı
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// Uygulamanın adı
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Uygulamanın DNS adı
        /// </summary>
        public string DnsName { get; set; }
        
        /// <summary>
        /// Uygulamanın kullandığı protokol
        /// </summary>
        public string Protocol { get; set; }
        
        /// <summary>
        /// Uygulamanın port numarası
        /// </summary>
        public int Port { get; set; }
        
        /// <summary>
        /// Uygulamanın versiyonu
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Uygulamanın durumu
        /// </summary>
        public ApplicationStatus Status { get; set; }
        
        /// <summary>
        /// Uygulamadan sorumlu kişi
        /// </summary>
        public string ResponsiblePerson { get; set; }
        
        /// <summary>
        /// Uygulama hakkında açıklama
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Uygulamanın çalıştığı sunucunun ID'si
        /// </summary>
        public Guid ServerId { get; set; }
        
        /// <summary>
        /// Uygulamanın çalıştığı sunucunun adı
        /// </summary>
        public string ServerName { get; set; }
        
        /// <summary>
        /// Uygulamanın oluşturulma tarihi
        /// </summary>
        public DateTime CreatedDate { get; set; }
        
        /// <summary>
        /// Uygulamanın son güncellenme tarihi
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
        
        /// <summary>
        /// Uygulamaya bağlı veritabanlarının listesi
        /// </summary>
        public List<DatabaseDto> ConnectedDatabases { get; set; }
        
        /// <summary>
        /// ApplicationDto'nun yeni bir örneğini oluşturur
        /// </summary>
        public ApplicationDto()
        {
            ConnectedDatabases = new List<DatabaseDto>();
        }
    }
} 