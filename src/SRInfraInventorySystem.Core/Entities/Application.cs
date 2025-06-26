using SRInfraInventorySystem.Core.Common;
using SRInfraInventorySystem.Core.Enums;
using System;
using System.Collections.Generic;

namespace SRInfraInventorySystem.Core.Entities
{
    /// <summary>
    /// Sunucularda çalışan uygulamaları temsil eden entity.
    /// Uygulama bilgileri, erişim detayları ve ilişkili kaynakları içerir.
    /// </summary>
    public class Application : BaseEntity
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
        /// Uygulamanın kullandığı protokol (HTTP, HTTPS, FTP vb.)
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
        /// Uygulamanın durumu (Active, Inactive, Maintenance)
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
        
        // Navigation Properties
        
        /// <summary>
        /// Uygulamanın çalıştığı sunucu
        /// </summary>
        public Server Server { get; set; }
        
        /// <summary>
        /// Uygulamaya bağlı veritabanlarının koleksiyonu
        /// </summary>
        public ICollection<Database> ConnectedDatabases { get; set; }
        
        /// <summary>
        /// Uygulamaya yapılan erişim loglarının koleksiyonu
        /// </summary>
        public ICollection<AccessLog> AccessLogs { get; set; }
        
        /// <summary>
        /// Application entity'sinin yeni bir örneğini oluşturur
        /// </summary>
        public Application()
        {
            ConnectedDatabases = new HashSet<Database>();
            AccessLogs = new HashSet<AccessLog>();
        }
    }
} 