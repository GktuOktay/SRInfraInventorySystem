using SRInfraInventorySystem.Core.Enums;
using System;

namespace SRInfraInventorySystem.Application.DTOs
{
    /// <summary>
    /// Yeni uygulama oluşturmak için kullanılan DTO.
    /// </summary>
    public class CreateApplicationDto
    {
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
    }
} 